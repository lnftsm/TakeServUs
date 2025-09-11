using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Persistence.Configurations;

public class QuoteItemConfiguration : IEntityTypeConfiguration<QuoteItem>
{
  public void Configure(EntityTypeBuilder<QuoteItem> builder)
  {
    builder.ToTable("quote_items");

    builder.HasKey(i => i.Id);

    builder.Property(i => i.Description)
        .IsRequired()
        .HasMaxLength(500);

    builder.Property(i => i.Quantity)
        .HasColumnType("decimal(18,2)")
        .IsRequired();

    // Money VO'larını yapılandırma
    builder.OwnsOne(i => i.UnitPrice, sb =>
    {
      sb.Property(m => m.Amount).HasColumnName("unit_price_amount").HasColumnType("decimal(18,2)");
      sb.Property(m => m.Currency).HasColumnName("unit_price_currency");
    });

    builder.OwnsOne(i => i.TotalPrice, sb =>
    {
      sb.Property(m => m.Amount).HasColumnName("total_price_amount").HasColumnType("decimal(18,2)");
      sb.Property(m => m.Currency).HasColumnName("total_price_currency");
    });

    // İlişki
    builder.HasOne(i => i.Quote)
        .WithMany(q => q.Items)
        .HasForeignKey(i => i.QuoteId);
  }
}
