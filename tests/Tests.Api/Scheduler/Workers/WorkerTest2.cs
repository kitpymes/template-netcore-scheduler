using Kitpymes.Core.Scheduler;
using System.Threading.Tasks;

namespace Tests.Api.Scheduler
{
    [WorkerID(WorkerId)]
    public class WorkerTest2 : IWorker
    {
        public const string WorkerId = nameof(WorkerTest2);

        public async ValueTask<bool> Execute(JobContext context)
        {
            var data = await context.GetDataAsync<WorkerTestData>();

            // Ejecutar trabajo...

            return true;
        }
    }
}
