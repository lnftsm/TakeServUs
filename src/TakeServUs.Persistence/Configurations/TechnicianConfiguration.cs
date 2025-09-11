using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Persistence.Configurations;

public class TechnicianConfiguration : IEntityTypeConfiguration<Technician>
{
  public void Configure(EntityTypeBuilder<Technician> builder)
  {
    builder.ToTable("technicians");

    builder.HasKey(t => t.Id);

    builder.Property(t => t.EmployeeCode).HasMaxLength(50);
    builder.Property(t => t.Specialization).HasMaxLength(200);

    // Current location (GpsCoordinates VO) is stored denormalized
    // Note: This leads to data duplication. Ideally, a reference to the latest Location record
    // should be used. This is an alternative for performance optimization.
    builder.OwnsOne(t => t.CurrentLocation, gps =>
    {
      gps.Property(g => g.Latitude).HasColumnName("current_latitude").HasColumnType("decimal(9, 6)");
      gps.Property(g => g.Longitude).HasColumnName("current_longitude").HasColumnType("decimal(9, 6)");
      gps.Property(g => g.Timestamp).HasColumnName("last_location_update");
      gps.Property(g => g.Accuracy).HasColumnName("current_accuracy");
    });

    var jsonSerializerOptions = new JsonSerializerOptions();

    builder.Property(t => t.Skills)
      .HasColumnName("skills")
      .HasColumnType("jsonb")
      .HasConversion(
          v => JsonSerializer.Serialize(v, jsonSerializerOptions),
          v => JsonSerializer.Deserialize<List<Skill>>(v, jsonSerializerOptions) ?? new List<Skill>(),
          new ValueComparer<ICollection<Skill>>(
              (c1, c2) => (c1 == null && c2 == null) || (c1 != null && c2 != null && c1.SequenceEqual(c2)),
              c => c == null ? 0 : c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
              c => c == null ? new List<Skill>() : c.ToList()));

    builder.Property(t => t.WorkingSchedule)
      .HasColumnName("working_schedule")
      .HasColumnType("jsonb")
      .HasConversion(
          v => JsonSerializer.Serialize(v, jsonSerializerOptions),
          v => JsonSerializer.Deserialize<List<DailySchedule>>(v, jsonSerializerOptions) ?? new List<DailySchedule>(),
          new ValueComparer<ICollection<DailySchedule>>(
              (c1, c2) => (c1 == null && c2 == null) || (c1 != null && c2 != null && c1.SequenceEqual(c2)),
              c => c == null ? 0 : c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
              c => c == null ? new List<DailySchedule>() : c.ToList()));

    // Soft Delete
    builder.HasQueryFilter(t => !t.IsDeleted);

    // Relationships
    builder.HasOne(t => t.User)
        .WithOne()
        .HasForeignKey<Technician>(t => t.UserId)
        .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(t => t.Company)
        .WithMany(c => c.Technicians)
        .HasForeignKey(t => t.CompanyId);

    builder.HasMany(t => t.LocationHistory)
        .WithOne(l => l.Technician)
        .HasForeignKey(l => l.TechnicianId);
  }
}
