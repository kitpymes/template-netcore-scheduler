// -----------------------------------------------------------------------
// <copyright file="ServiceCollectionsExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Scheduler
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Kitpymes.Core.Shared;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddScheduler<TDbContext>(this IServiceCollection services, Assembly assembly, Action<SchedulerOptions>? action = null)
            where TDbContext : DbContext, IDbContextScheduler
        {
            services.AddSingleton<ISchedulerService, SchedulerService>();
            services
                .AddWorkers(assembly)
                .Startup<TDbContext>(action);

            return services;
        }

        private static IServiceCollection AddWorkers(this IServiceCollection services, Assembly assembly)
        {
            var schedulerService = services.ToService<ISchedulerService>();

            var types = schedulerService?.DiscoverWorkers(assembly);

            if (types is not null && types.Any())
            {
                foreach (Type type in types)
                {
                    services.AddSingleton(typeof(IWorker), type);

                    schedulerService?.RegisterWorker(type);
                }
            }

            return services;
        }

        private static void Startup<TDbContext>(this IServiceCollection services, Action<SchedulerOptions>? action = null)
            where TDbContext : DbContext, IDbContextScheduler
        => services.AddHostedService(x => new Startup<TDbContext>(services, action));
    }
}
