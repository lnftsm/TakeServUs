using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Infrastructure.Persistence.Configurations;

public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
{
  public void Configure(EntityTypeBuilder<Quote> builder)
  {
    builder.ToTable("Quotes");
    builder.HasKey(q => q.Id);

    builder.Property(q => q.QuoteNumber).IsRequired().HasMaxLength(50);

    builder.OwnsOne(q => q.SubTotal, moneyBuilder =>
    {
      moneyBuilder.Property(m => m.Amount).HasColumnType("decimal(18,2)").HasColumnName("SubTotal");
      moneyBuilder.Property(m => m.Currency).HasColumnName("SubTotalCurrency");
    });
    builder.OwnsOne(q => q.TaxAmount, moneyBuilder =>
    {
      moneyBuilder.Property(m => m.Amount).HasColumnType("decimal(18,2)").HasColumnName("TaxAmount");
      moneyBuilder.Property(m => m.Currency).HasColumnName("TaxAmountCurrency");
    });
    builder.OwnsOne(q => q.DiscountAmount, moneyBuilder =>
    {
      moneyBuilder.Property(m => m.Amount).HasColumnType("decimal(18,2)").HasColumnName("DiscountAmount");
      moneyBuilder.Property(m => m.Currency).HasColumnName("DiscountAmountCurrency");
    });
    builder.OwnsOne(q => q.TotalAmount, moneyBuilder =>
    {
      moneyBuilder.Property(m => m.Amount).HasColumnType("decimal(18,2)").HasColumnName("TotalAmount");
      moneyBuilder.Property(m => m.Currency).HasColumnName("TotalAmountCurrency");
    });

    builder.HasOne(q => q.Job).WithOne(j => j.Quote).HasForeignKey<Quote>(q => q.JobId);
    builder.HasMany(q => q.Items).WithOne(i => i.Quote).HasForeignKey(i => i.QuoteId);
  }
}
