namespace Kitpymes.Core.Scheduler
{
    public class SchedulerOptions
    {
        public readonly SchedulerSettings Settings = new ();

        public SchedulerOptions AddStartupDelay(int startupDelaySeconds = SchedulerSettings.DefaultStartupDelaySeconds)
        {
            Settings.StartupDelaySeconds = startupDelaySeconds;

            return this;
        }

        public SchedulerOptions AddSchedulingIntervalSeconds(int schedulingIntervalSeconds = SchedulerSettings.DefaultSchedulingIntervalSeconds)
        {
            Settings.SchedulingIntervalSeconds = schedulingIntervalSeconds;

            return this;
        }

        public SchedulerOptions AddMaxJobsRetrieval(int maxJobsRetrieval = SchedulerSettings.DefaultMaxJobsRetrieval)
        {
            Settings.MaxJobsRetrieval = maxJobsRetrieval;

            return this;
        }
    }
}
