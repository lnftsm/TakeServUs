namespace TakeServUs.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;
using TakeServUs.Domain.ValueObjects;
public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
  public void Configure(EntityTypeBuilder<Company> builder)
  {
    builder.ToTable("companies");

    builder.HasKey(c => c.Id);

    builder.Property(c => c.Name)
        .IsRequired()
        .HasMaxLength(200);

    builder.Property(c => c.Email)
        .IsRequired()
        .HasMaxLength(256)
        .HasConversion(
            v => v.Value,
            v => new Email(v));

    builder.Property(c => c.TimeZone)
        .IsRequired()
        .HasMaxLength(100)
        .HasDefaultValue("UTC");

    // Address Value Object configuration
    builder.OwnsOne(c => c.Address, addressBuilder =>
     {
       addressBuilder.Property(a => a.Street).HasColumnName("address_street").HasMaxLength(255);
       addressBuilder.Property(a => a.City).HasColumnName("address_city").HasMaxLength(100);
       addressBuilder.Property(a => a.State).HasColumnName("address_state").HasMaxLength(100);
       addressBuilder.Property(a => a.PostalCode).HasColumnName("address_postal_code").HasMaxLength(20);
       addressBuilder.Property(a => a.Country).HasColumnName("address_country").HasMaxLength(100);

       addressBuilder.OwnsOne(a => a.Coordinates, gpsBuilder =>
           {
             gpsBuilder.Property(g => g.Latitude).HasColumnName("address_latitude").HasColumnType("decimal(9, 6)");
             gpsBuilder.Property(g => g.Longitude).HasColumnName("address_longitude").HasColumnType("decimal(9, 6)");
             gpsBuilder.Property(g => g.Timestamp).HasColumnName("address_gps_timestamp");
             gpsBuilder.Property(g => g.Accuracy).HasColumnName("address_gps_accuracy");
           });
     });


    builder.OwnsOne(c => c.PhoneNumber, phoneBuilder =>
    {
      phoneBuilder.Property(p => p.CountryCode).HasColumnName("phone_country_code").HasMaxLength(5);
      phoneBuilder.Property(p => p.Number).HasColumnName("phone_number").HasMaxLength(15);
    });

    // Soft Delete filter
    builder.HasQueryFilter(c => !c.IsDeleted);

    // Relationships
    builder.HasMany(c => c.Users)
        .WithOne(u => u.Company)
        .HasForeignKey(u => u.CompanyId)
        .OnDelete(DeleteBehavior.Restrict);

    builder.HasMany(c => c.Technicians)
        .WithOne(t => t.Company)
        .HasForeignKey(t => t.CompanyId)
        .OnDelete(DeleteBehavior.Cascade);

    builder.HasMany(c => c.Jobs)
        .WithOne(j => j.Company)
        .HasForeignKey(j => j.CompanyId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
