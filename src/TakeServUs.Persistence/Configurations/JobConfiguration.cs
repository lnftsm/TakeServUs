using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Infrastructure.Persistence.Configurations;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
  public void Configure(EntityTypeBuilder<Job> builder)
  {
    builder.ToTable("Jobs");
    builder.HasKey(j => j.Id);

    builder.Property(j => j.JobNumber).IsRequired().HasMaxLength(50);
    builder.Property(j => j.Title).IsRequired().HasMaxLength(200);

    builder.OwnsOne(j => j.ServiceAddress, addressBuilder =>
    {
      addressBuilder.Property(a => a.Street).IsRequired().HasMaxLength(200).HasColumnName("ServiceStreet");
      addressBuilder.Property(a => a.City).IsRequired().HasMaxLength(100).HasColumnName("ServiceCity");
      addressBuilder.Property(a => a.State).IsRequired().HasMaxLength(100).HasColumnName("ServiceState");
      addressBuilder.Property(a => a.PostalCode).IsRequired().HasMaxLength(20).HasColumnName("ServicePostalCode");
      addressBuilder.Property(a => a.Country).IsRequired().HasMaxLength(100).HasColumnName("ServiceCountry");
    });

    builder.OwnsOne(j => j.EstimatedCost, moneyBuilder =>
    {
      moneyBuilder.Property(m => m.Amount).HasColumnType("decimal(18,2)").HasColumnName("EstimatedCost");
      moneyBuilder.Property(m => m.Currency).HasColumnName("EstimatedCostCurrency");
    });
    builder.OwnsOne(j => j.ActualCost, moneyBuilder =>
    {
      moneyBuilder.Property(m => m.Amount).HasColumnType("decimal(18,2)").HasColumnName("ActualCost");
      moneyBuilder.Property(m => m.Currency).HasColumnName("ActualCostCurrency");
    });

    builder.HasMany(j => j.JobAssignments).WithOne(a => a.Job).HasForeignKey(a => a.JobId);
    builder.HasMany(j => j.StatusHistory).WithOne(s => s.Job).HasForeignKey(s => s.JobId);
    builder.HasMany(j => j.JobMaterials).WithOne(m => m.Job).HasForeignKey(m => m.JobId);
  }
}
