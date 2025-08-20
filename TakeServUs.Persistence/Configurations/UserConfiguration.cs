using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("Users");
    builder.HasKey(u => u.Id);

    builder.Property(u => u.PasswordHash).IsRequired();

    builder.OwnsOne(u => u.FullName, nameBuilder =>
    {
      nameBuilder.Property(n => n.FirstName).IsRequired().HasMaxLength(100).HasColumnName("FirstName");
      nameBuilder.Property(n => n.LastName).IsRequired().HasMaxLength(100).HasColumnName("LastName");
      nameBuilder.Property(n => n.MiddleName).HasMaxLength(100).HasColumnName("MiddleName");
    });

    builder.OwnsOne(u => u.Email, emailBuilder =>
    {
      emailBuilder.Property(e => e.Value).IsRequired().HasMaxLength(256).HasColumnName("Email");
    });

    builder.OwnsOne(u => u.PhoneNumber, phoneBuilder =>
    {
      phoneBuilder.Property(p => p.CountryCode).IsRequired().HasMaxLength(4).HasColumnName("PhoneNumberCountryCode");
      phoneBuilder.Property(p => p.Number).IsRequired().HasMaxLength(15).HasColumnName("PhoneNumber");
    });
  }
}
