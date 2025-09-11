using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Persistence.Configurations;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
  public void Configure(EntityTypeBuilder<Job> builder)
  {
    builder.ToTable("jobs");

    builder.HasKey(j => j.Id);

    // JobNumber VO'sunu tek kolona dönüştürme
    builder.Property(j => j.JobNumber)
        .IsRequired()
        .HasMaxLength(50);

    builder.Property(j => j.Title)
        .IsRequired()
        .HasMaxLength(250);

    // ServiceAddress (Address VO) yapılandırması
    builder.OwnsOne(j => j.ServiceAddress, addressBuilder =>
    {
      addressBuilder.Property(a => a.Street).HasColumnName("service_address_street").HasMaxLength(255);
      addressBuilder.Property(a => a.City).HasColumnName("service_address_city").HasMaxLength(100);
      addressBuilder.Property(a => a.State).HasColumnName("service_address_state").HasMaxLength(100);
      addressBuilder.Property(a => a.PostalCode).HasColumnName("service_address_postal_code").HasMaxLength(20);
      addressBuilder.Property(a => a.Country).HasColumnName("service_address_country").HasMaxLength(100);

      addressBuilder.OwnsOne(a => a.Coordinates, gpsBuilder =>
          {
            gpsBuilder.Property(g => g.Latitude).HasColumnName("service_address_latitude").HasColumnType("decimal(9, 6)");
            gpsBuilder.Property(g => g.Longitude).HasColumnName("service_address_longitude").HasColumnType("decimal(9, 6)");
            gpsBuilder.Property(g => g.Timestamp).HasColumnName("service_address_gps_timestamp");
            gpsBuilder.Property(g => g.Accuracy).HasColumnName("service_address_gps_accuracy");
          });
    });

    // Money VO'larını yapılandırma
    builder.OwnsOne(j => j.EstimatedCost, moneyBuilder =>
    {
      moneyBuilder.Property(m => m.Amount).HasColumnName("estimated_cost_amount").HasColumnType("decimal(18,2)");
      moneyBuilder.Property(m => m.Currency).HasColumnName("estimated_cost_currency");
    });

    builder.OwnsOne(j => j.ActualCost, moneyBuilder =>
    {
      moneyBuilder.Property(m => m.Amount).HasColumnName("actual_cost_amount").HasColumnType("decimal(18,2)");
      moneyBuilder.Property(m => m.Currency).HasColumnName("actual_cost_currency");
    });

    // ICollection<string> alanlarını JSON'a dönüştürme
    var jsonSerializerOptions = new JsonSerializerOptions();
    builder.Property(j => j.PhotoUrls)
        .HasConversion(
            v => JsonSerializer.Serialize(v, jsonSerializerOptions),
            v => JsonSerializer.Deserialize<List<string>>(v, jsonSerializerOptions) ?? new List<string>(),
            new ValueComparer<ICollection<string>>(
                (c1, c2) => c1!.SequenceEqual(c2!),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()));

    builder.Property(j => j.Attachments)
        .HasConversion(
            v => JsonSerializer.Serialize(v, jsonSerializerOptions),
            v => JsonSerializer.Deserialize<List<string>>(v, jsonSerializerOptions) ?? new List<string>(),
            new ValueComparer<ICollection<string>>(
                (c1, c2) => c1!.SequenceEqual(c2!),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()));

    // Soft Delete
    builder.HasQueryFilter(j => !j.IsDeleted);

    // İlişkiler
    builder.HasOne(j => j.Company)
        .WithMany(c => c.Jobs)
        .HasForeignKey(j => j.CompanyId)
        .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(j => j.Customer)
        .WithMany(c => c.Jobs)
        .HasForeignKey(j => j.CustomerId)
        .OnDelete(DeleteBehavior.Restrict);

    builder.HasMany(j => j.StatusHistory)
        .WithOne(s => s.Job)
        .HasForeignKey(s => s.JobId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
