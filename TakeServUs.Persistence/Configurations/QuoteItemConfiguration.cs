using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Infrastructure.Persistence.Configurations;

public class QuoteItemConfiguration : IEntityTypeConfiguration<QuoteItem>
{
  public void Configure(EntityTypeBuilder<QuoteItem> builder)
  {
    builder.ToTable("QuoteItems");
    builder.HasKey(qi => qi.Id);

    builder.OwnsOne(qi => qi.UnitPrice, moneyBuilder =>
   {
     moneyBuilder.Property(m => m.Amount).HasColumnType("decimal(18,2)").HasColumnName("UnitPrice");
     moneyBuilder.Property(m => m.Currency).HasColumnName("UnitPriceCurrency");
   });
    builder.OwnsOne(qi => qi.TotalPrice, moneyBuilder =>
    {
      moneyBuilder.Property(m => m.Amount).HasColumnType("decimal(18,2)").HasColumnName("TotalPrice");
      moneyBuilder.Property(m => m.Currency).HasColumnName("TotalPriceCurrency");
    });
  }
}
