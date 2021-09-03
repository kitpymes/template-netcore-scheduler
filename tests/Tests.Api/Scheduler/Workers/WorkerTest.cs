using Kitpymes.Core.Scheduler;
using System.Threading.Tasks;

namespace Tests.Api.Scheduler
{
    [WorkerID(WorkerId)]
    public class WorkerTest : IWorker
    {
        public const string WorkerId = nameof(WorkerTest);

        public async ValueTask<bool> Execute(JobContext context)
        {
            var data = await context.GetDataAsync<WorkerTestData>();

            // Ejecutar trabajo...

            return true; 
        }
    }
}
