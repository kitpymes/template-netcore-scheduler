namespace Tests.Application
{
    public class JobDto
    {
        public long Id { get; set; }

        public string GroupId { get; set; }

        public string WorkerId { get; set; }

        public string Data { get; set; }

        public string State { get; set; }

        public int RetriesCounter { get; set; }

        public int MaxRetries { get; set; }

        public string ScheduledAt { get; set; }

        public int UserId { get; set; }

        public string Description { get; set; }

        public string ExecutedAt { get; set; }
    }
}
