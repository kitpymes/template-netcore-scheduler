namespace Kitpymes.Core.Scheduler
{
    public class SchedulerSettings
    {
        public const int DefaultStartupDelaySeconds = 10;
        public const int DefaultSchedulingIntervalSeconds = 60;
        public const int DefaultMaxJobsRetrieval = 5;

        private int _startupDelaySeconds = DefaultStartupDelaySeconds;
        private int _schedulingIntervalSeconds = DefaultSchedulingIntervalSeconds;
        private int _maxJobsRetrieval = DefaultMaxJobsRetrieval;

        public int? StartupDelaySeconds
        {
            get => _startupDelaySeconds;
            set
            {
                if (value.HasValue && value > 0)
                {
                    _startupDelaySeconds = value.Value;
                }
            }
        }

        public int? SchedulingIntervalSeconds
        {
            get => _schedulingIntervalSeconds;
            set
            {
                if (value.HasValue && value > 0)
                {
                    _schedulingIntervalSeconds = value.Value;
                }
            }
        }

        public int? MaxJobsRetrieval
        {
            get => _maxJobsRetrieval;
            set
            {
                if (value.HasValue && value > 0)
                {
                    _maxJobsRetrieval = value.Value;
                }
            }
        }
    }
}
