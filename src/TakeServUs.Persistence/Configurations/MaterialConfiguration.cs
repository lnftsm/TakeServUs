using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Infrastructure.Persistence.Configurations;

public class MaterialConfiguration : IEntityTypeConfiguration<Material>
{
  public void Configure(EntityTypeBuilder<Material> builder)
  {
    builder.ToTable("Materials");
    builder.HasKey(m => m.Id);
    builder.Property(m => m.Code).IsRequired().HasMaxLength(50);
    builder.Property(m => m.Name).IsRequired().HasMaxLength(200);

    builder.OwnsOne(m => m.UnitCost, moneyBuilder =>
    {
      moneyBuilder.Property(p => p.Amount).HasColumnType("decimal(18,2)").HasColumnName("UnitCost");
      moneyBuilder.Property(p => p.Currency).HasColumnName("UnitCostCurrency");
    });

    builder.OwnsOne(m => m.UnitPrice, moneyBuilder =>
    {
      moneyBuilder.Property(p => p.Amount).HasColumnType("decimal(18,2)").HasColumnName("UnitPrice");
      moneyBuilder.Property(p => p.Currency).HasColumnName("UnitPriceCurrency");
    });

    builder.Property(m => m.QuantityInStock).HasColumnType("decimal(18,2)");
  }
}
