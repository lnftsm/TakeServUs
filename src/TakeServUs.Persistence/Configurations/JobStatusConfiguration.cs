using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Infrastructure.Persistence.Configurations;

public class JobStatusConfiguration : IEntityTypeConfiguration<JobStatus>
{
  public void Configure(EntityTypeBuilder<JobStatus> builder)
  {
    builder.ToTable("JobStatusHistory");
    builder.HasKey(js => js.Id);

    builder.HasOne(js => js.Job).WithMany(j => j.StatusHistory).HasForeignKey(js => js.JobId);
  }
}
