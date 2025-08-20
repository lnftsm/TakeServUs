using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Infrastructure.Persistence.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
  public void Configure(EntityTypeBuilder<Company> builder)
  {
    builder.ToTable("Companies");
    builder.HasKey(c => c.Id);

    builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
    builder.Property(c => c.Email).IsRequired().HasMaxLength(256);

    builder.OwnsOne(c => c.Address, addressBuilder =>
    {
      addressBuilder.Property(a => a.Street).HasMaxLength(200).HasColumnName("Street");
      addressBuilder.Property(a => a.City).HasMaxLength(100).HasColumnName("City");
      addressBuilder.Property(a => a.State).HasMaxLength(100).HasColumnName("State");
      addressBuilder.Property(a => a.PostalCode).HasMaxLength(20).HasColumnName("PostalCode");
      addressBuilder.Property(a => a.Country).HasMaxLength(100).HasColumnName("Country");
    });

    builder.OwnsOne(c => c.PhoneNumber, phoneBuilder =>
    {
      phoneBuilder.Property(p => p.CountryCode).HasMaxLength(4).HasColumnName("PhoneNumberCountryCode");
      phoneBuilder.Property(p => p.Number).HasMaxLength(15).HasColumnName("PhoneNumber");
    });

    builder.HasMany(c => c.Users).WithOne(u => u.Company).HasForeignKey(u => u.CompanyId);
    builder.HasMany(c => c.Technicians).WithOne(t => t.Company).HasForeignKey(t => t.CompanyId);
    builder.HasMany(c => c.Jobs).WithOne(j => j.Company).HasForeignKey(j => j.CompanyId);
    builder.HasMany(c => c.Customers).WithOne(cust => cust.Company).HasForeignKey(cust => cust.CompanyId);
    builder.HasMany(c => c.Materials).WithOne(m => m.Company).HasForeignKey(m => m.CompanyId);
  }
}
