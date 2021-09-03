namespace Tests.Persistence
{
    using Kitpymes.Core.Scheduler;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    public class AppDBContext : DbContext, IDbContextScheduler
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options) { }

        public DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
