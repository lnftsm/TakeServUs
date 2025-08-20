using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
  public void Configure(EntityTypeBuilder<Customer> builder)
  {
    builder.ToTable("Customers");
    builder.HasKey(c => c.Id);

    builder.OwnsOne(c => c.Address, addressBuilder =>
    {
      addressBuilder.Property(a => a.Street).IsRequired().HasMaxLength(200).HasColumnName("Street");
      addressBuilder.Property(a => a.City).IsRequired().HasMaxLength(100).HasColumnName("City");
      addressBuilder.Property(a => a.State).IsRequired().HasMaxLength(100).HasColumnName("State");
      addressBuilder.Property(a => a.PostalCode).IsRequired().HasMaxLength(20).HasColumnName("PostalCode");
      addressBuilder.Property(a => a.Country).IsRequired().HasMaxLength(100).HasColumnName("Country");
    });

    builder.Property(c => c.CreditLimit).HasColumnType("decimal(18,2)");
    builder.Property(c => c.CurrentBalance).HasColumnType("decimal(18,2)");

    builder.HasOne(c => c.User).WithOne().HasForeignKey<Customer>(c => c.UserId);
    builder.HasOne(c => c.Company).WithMany(comp => comp.Customers).HasForeignKey(c => c.CompanyId);
  }
}
