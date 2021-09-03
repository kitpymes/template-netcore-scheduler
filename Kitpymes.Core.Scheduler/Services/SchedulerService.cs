namespace Kitpymes.Core.Scheduler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;

    public class SchedulerService : ISchedulerService
    {
        private static IDictionary<string, Type> _workers;

        private readonly IServiceProvider _provider;

        public SchedulerService(IServiceProvider provider)
        {
            _provider = provider;
            _workers ??= new Dictionary<string, Type>();
        }

        public void RegisterWorker(Type type)
        {
            if (!typeof(IWorker).IsAssignableFrom(type))
            {
                throw new SchedulerException(ErrorMsg.NotImplementIWorker(type));
            }

            string? workerId = this.GetWorkerId(type);

            if (string.IsNullOrWhiteSpace(workerId))
            {
                throw new SchedulerException(ErrorMsg.WorkerIdNotFound());
            }

            if (_workers.TryGetValue(workerId, out _))
            {
                throw new SchedulerException(ErrorMsg.DuplicatedWorkerId(workerId));
            }

            _workers.Add(workerId, type);
        }

        public IWorker? GetWorker(string workerId)
        {
            if (_workers.TryGetValue(workerId, out _))
            {
                var ws = _provider.GetServices<IWorker>();

                foreach (var w in ws)
                {
                    string? id = GetWorkerId(w.GetType());

                    if (id == workerId)
                    {
                        return w;
                    }
                }
            }

            return null;
        }

        public IEnumerable<Type> DiscoverWorkers(Assembly assembly)
        => assembly.GetTypes()
            .Where(t => t.GetInterface(typeof(IWorker).FullName) == typeof(IWorker) && !t.IsInterface);

        private string? GetWorkerId(Type type)
        {
            var attribute = type?
                .GetCustomAttributes(typeof(WorkerIDAttribute), false)
                .FirstOrDefault() as WorkerIDAttribute;

            return attribute?.WorkerID;
        }
    }
}
