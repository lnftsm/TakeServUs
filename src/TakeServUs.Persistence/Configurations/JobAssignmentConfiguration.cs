using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Infrastructure.Persistence.Configurations;

public class JobAssignmentConfiguration : IEntityTypeConfiguration<JobAssignment>
{
  public void Configure(EntityTypeBuilder<JobAssignment> builder)
  {
    builder.ToTable("JobAssignments");
    builder.HasKey(ja => ja.Id);

    builder.HasOne(ja => ja.Job).WithMany(j => j.JobAssignments).HasForeignKey(ja => ja.JobId);
    builder.HasOne(ja => ja.Technician).WithMany(t => t.JobAssignments).HasForeignKey(ja => ja.TechnicianId);
  }
}
