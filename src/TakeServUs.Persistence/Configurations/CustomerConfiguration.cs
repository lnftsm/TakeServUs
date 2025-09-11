namespace TakeServUs.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
  public void Configure(EntityTypeBuilder<Customer> builder)
  {
    builder.ToTable("customers");

    builder.HasKey(c => c.Id);

    builder.Property(c => c.CompanyName).HasMaxLength(200);
    builder.Property(c => c.CreditLimit).HasColumnType("decimal(18,2)");
    builder.Property(c => c.CurrentBalance).HasColumnType("decimal(18,2)").HasDefaultValue(0);

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

    // Soft Delete filter
    builder.HasQueryFilter(c => !c.IsDeleted);

    // Relationships
    builder.HasOne(c => c.User)
        .WithMany()
        .HasForeignKey(c => c.UserId)
        .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(c => c.Company)
        .WithMany(co => co.Customers)
        .HasForeignKey(c => c.CompanyId)
        .OnDelete(DeleteBehavior.Cascade);

    builder.HasMany(c => c.Jobs)
        .WithOne(j => j.Customer)
        .HasForeignKey(j => j.CustomerId)
        .OnDelete(DeleteBehavior.Restrict);
  }
}
