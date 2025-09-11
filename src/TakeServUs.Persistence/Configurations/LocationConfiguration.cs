using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Persistence.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
  public void Configure(EntityTypeBuilder<Location> builder)
  {
    builder.ToTable("locations");

    builder.HasKey(l => l.Id);

    // GpsCoordinates VO'sunu owned type olarak yapılandırma
    // Bu, bu VO için en ideal kullanım senaryosudur.
    builder.OwnsOne(l => l.Coordinate, gps =>
    {
      gps.Property(g => g.Latitude).IsRequired().HasColumnName("latitude").HasColumnType("decimal(9, 6)");
      gps.Property(g => g.Longitude).IsRequired().HasColumnName("longitude").HasColumnType("decimal(9, 6)");
      gps.Property(g => g.Timestamp).IsRequired().HasColumnName("recorded_at");
      gps.Property(g => g.Accuracy).HasColumnName("accuracy");
    });

    builder.Property(l => l.ActivityType).HasMaxLength(50);

    // İlişki
    builder.HasOne(l => l.Technician)
        .WithMany(t => t.LocationHistory)
        .HasForeignKey(l => l.TechnicianId);
  }
}
