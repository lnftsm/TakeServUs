using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Persistence.Configurations;

public class JobStatusConfiguration : IEntityTypeConfiguration<JobStatus>
{
  public void Configure(EntityTypeBuilder<JobStatus> builder)
  {
    builder.ToTable("job_statuses");

    builder.HasKey(js => js.Id);

    builder.Property(js => js.Status).IsRequired();
    builder.Property(js => js.ChangedBy).HasMaxLength(256);

    // Durum değişikliğinin yapıldığı konumu GpsCoordinates VO'su ile saklama
    // Not: JobStatus entity'nizin double? Latitude/Longitude yerine
    // GpsCoordinates Location { get; set; } alanını kullanacak şekilde
    // güncellendiği varsayılmıştır.
    builder.OwnsOne(js => js.Location, gps =>
    {
      gps.Property(g => g.Latitude).HasColumnName("latitude").HasColumnType("decimal(9, 6)");
      gps.Property(g => g.Longitude).HasColumnName("longitude").HasColumnType("decimal(9, 6)");
      gps.Property(g => g.Timestamp).HasColumnName("location_timestamp");
      gps.Property(g => g.Accuracy).HasColumnName("location_accuracy");
    });

    // İlişki
    builder.HasOne(js => js.Job)
        .WithMany(j => j.StatusHistory)
        .HasForeignKey(js => js.JobId);
  }
}
