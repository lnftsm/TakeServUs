using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Persistence.Configurations;

public class JobAssignmentConfiguration : IEntityTypeConfiguration<JobAssignment>
{
  public void Configure(EntityTypeBuilder<JobAssignment> builder)
  {
    builder.ToTable("job_assignments");

    builder.HasKey(ja => ja.Id);

    // Bir teknisyenin bir işe yalnızca bir kez atanabilmesini sağlar
    builder.HasIndex(ja => new { ja.JobId, ja.TechnicianId }).IsUnique();

    // İlişkiler
    builder.HasOne(ja => ja.Job)
        .WithMany(j => j.JobAssignments)
        .HasForeignKey(ja => ja.JobId)
        .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(ja => ja.Technician)
        .WithMany(t => t.JobAssignments)
        .HasForeignKey(ja => ja.TechnicianId)
        .OnDelete(DeleteBehavior.Restrict); // Teknisyen silinirse atamaları silinmesin
  }
}
