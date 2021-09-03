using Kitpymes.Core.Scheduler;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tests.Api.Proyects.Persistence.Configuration
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.GroupId, x.WorkerId }, "Unique_GroupId_WorkerId").IsUnique(true);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.WorkerId).IsRequired().HasMaxLength(50);

            builder.Property(x => x.Data).IsRequired().HasMaxLength(200);

            builder.Property(x => x.State).IsRequired().HasMaxLength(50);

            builder.Property(x => x.RetriesCounter).IsRequired();

            builder.Property(x => x.MaxRetries).IsRequired();

            builder.Property(x => x.ScheduledAt).IsRequired();

            builder.Property(x => x.ExecutedAt).IsRequired(false);

            builder.Property(x => x.GroupId).IsRequired(false).HasMaxLength(50);

            builder.Property(x => x.UserId).IsRequired(false);

            builder.Property(x => x.Description).IsRequired(false);
        }
    }
}
