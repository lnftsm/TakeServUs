using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("users");

    builder.HasKey(u => u.Id);

    //builder.HasIndex(u => u.Email.Value).IsUnique(); // Unique index on Email

    // Configure Email Value Object
    builder.Property(u => u.Email)
        .IsRequired()
        .HasMaxLength(256)
        .HasConversion(
            email => email.Value,
            value => new Email(value));

    builder.Property(u => u.PasswordHash).IsRequired();

    // Configure FullName Value Object
    builder.OwnsOne(u => u.FullName, fn =>
    {
      fn.Property(f => f.FirstName).IsRequired().HasMaxLength(100).HasColumnName("first_name");
      fn.Property(f => f.LastName).IsRequired().HasMaxLength(100).HasColumnName("last_name");
      fn.Property(f => f.MiddleName).HasMaxLength(100).HasColumnName("middle_name");
    });

    // Configure PhoneNumber Value Object
    builder.OwnsOne(u => u.PhoneNumber, pn =>
    {
      pn.Property(p => p.CountryCode).IsRequired().HasMaxLength(5).HasColumnName("phone_country_code");
      pn.Property(p => p.Number).IsRequired().HasMaxLength(15).HasColumnName("phone_number");
    });

    builder.Property(u => u.PreferredLanguage)
        .IsRequired()
        .HasMaxLength(10)
        .HasDefaultValue("en");

    // Soft Delete filter
    builder.HasQueryFilter(u => !u.IsDeleted);

    // Relationships
    builder.HasOne(u => u.Company)
        .WithMany(c => c.Users)
        .HasForeignKey(u => u.CompanyId)
        .IsRequired(false);

    builder.HasMany(u => u.Notifications)
        .WithOne(n => n.User)
        .HasForeignKey(n => n.UserId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
