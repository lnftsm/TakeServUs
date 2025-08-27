using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Infrastructure.Persistence.Configurations;

public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
{
  public void Configure(EntityTypeBuilder<InvoiceItem> builder)
  {
    builder.ToTable("InvoiceItems");
    builder.HasKey(i => i.Id);

    builder.Property(i => i.Description).IsRequired().HasMaxLength(500);
    builder.Property(i => i.Quantity).HasColumnType("decimal(18,2)");

    builder.OwnsOne(i => i.UnitPrice, moneyBuilder =>
    {
      moneyBuilder.Property(m => m.Amount).HasColumnType("decimal(18,2)").HasColumnName("UnitPrice");
      moneyBuilder.Property(m => m.Currency).HasColumnName("UnitPriceCurrency");
    });
    builder.OwnsOne(i => i.TotalPrice, moneyBuilder =>
    {
      moneyBuilder.Property(m => m.Amount).HasColumnType("decimal(18,2)").HasColumnName("TotalPrice");
      moneyBuilder.Property(m => m.Currency).HasColumnName("TotalPriceCurrency");
    });
  }
}
