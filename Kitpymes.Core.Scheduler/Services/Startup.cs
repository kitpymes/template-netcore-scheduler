// -----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Scheduler
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Kitpymes.Core.Shared;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    internal class Startup<TDbContext> : IHostedService, IAsyncDisposable
        where TDbContext : DbContext, IDbContextScheduler
    {
        private readonly SchedulerSettings Settings = new ();
        private readonly IServiceCollection _services;
        private Timer? _timer;

        public Startup(IServiceCollection services, Action<SchedulerOptions>? action = null)
        {
            _services = services;

            Settings = action.ToConfigureOrDefault().Settings;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(StartJobs, null, TimeSpan.FromSeconds(Settings.StartupDelaySeconds!.Value), TimeSpan.FromSeconds(Settings.SchedulingIntervalSeconds!.Value));

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken) => await DisposeAsync();

        public async ValueTask DisposeAsync()
        {
            if (_timer is not null)
            {
                await _timer.DisposeAsync();
            }
        }

        private async void StartJobs(object? state) => _ = DoAsync();

        private async Task DoAsync()
        {
            //using var scope = _services.BuildServiceProvider().CreateScope();
            //var services = scope.ServiceProvider;
            //var logger = services.GetRequiredService<ILogger<Startup<TDbContext>>>();
            //var context = services.GetRequiredService<TDbContext>();

            var logger = _services.ToService<ILogger<Startup<TDbContext>>>();
            var context = _services.ToService<TDbContext>();

            if (context is not null)
            {
                var jobs = await context.Jobs
                    .Where(job => job.State == JobState.Scheduled.ToString() && job.RetriesCounter < job.MaxRetries)
                    .Take(Settings.MaxJobsRetrieval!.Value)
                    .OrderBy(x => x.ScheduledAt)
                    .ToListAsync();

                if (jobs.Any())
                {
                    //var workers = services.GetService<ISchedulerService>();

                    var workers = _services.ToService<ISchedulerService>();

                    foreach (var job in jobs)
                    {
                        bool succeeded = false;

                        var worker = workers?.GetWorker(job.WorkerId);

                        if (worker != null)
                        {
                            var jobContext = new JobContext(job.Id, job.GroupId, job.Data, DateTime.Now);

                            try
                            {
                                succeeded = await worker.Execute(jobContext);

                                job.ExecutedAt = DateTime.Now;

                                logger.LogInformation($"New job has been EXECUTED at {job.ExecutedAt.Value}: [{job.Id} - {job.WorkerId}]");
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, ErrorMsg.ExecuteWorker(job.WorkerId));
                            }
                        }
                        else
                        {
                            logger.LogError(ErrorMsg.WorkerIdNotFound(job.WorkerId));
                        }

                        if (succeeded)
                        {
                            job.State = JobState.Executed.ToString();
                        }
                        else
                        {
                            if (job.RetriesCounter < job.MaxRetries)
                            {
                                job.RetriesCounter += 1;
                                job.State = JobState.Scheduled.ToString();
                            }
                            else
                            {
                                job.State = JobState.Failed.ToString();
                            }
                        }

                        context.Jobs.Update(job);

                        await context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
