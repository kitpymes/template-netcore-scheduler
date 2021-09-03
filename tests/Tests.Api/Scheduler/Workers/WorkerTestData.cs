using System.ComponentModel.DataAnnotations;

namespace Tests.Api.Scheduler
{
    public class WorkerTestData
    {
        public WorkerTestData(int id, string value)
        {
            Id = id;
            Value = value;
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
