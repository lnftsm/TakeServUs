using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Infrastructure.Persistence.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
  public void Configure(EntityTypeBuilder<Invoice> builder)
  {
    builder.ToTable("Invoices");
    builder.HasKey(i => i.Id);

    builder.Property(i => i.InvoiceNumber).IsRequired().HasMaxLength(50);

    builder.OwnsOne(i => i.SubTotal, moneyBuilder =>
    {
      moneyBuilder.Property(m => m.Amount).HasColumnType("decimal(18,2)").HasColumnName("SubTotal");
      moneyBuilder.Property(m => m.Currency).HasColumnName("SubTotalCurrency");
    });
    builder.OwnsOne(i => i.TaxAmount, moneyBuilder =>
    {
      moneyBuilder.Property(m => m.Amount).HasColumnType("decimal(18,2)").HasColumnName("TaxAmount");
      moneyBuilder.Property(m => m.Currency).HasColumnName("TaxAmountCurrency");
    });
    builder.OwnsOne(i => i.DiscountAmount, moneyBuilder =>
    {
      moneyBuilder.Property(m => m.Amount).HasColumnType("decimal(18,2)").HasColumnName("DiscountAmount");
      moneyBuilder.Property(m => m.Currency).HasColumnName("DiscountAmountCurrency");
    });
    builder.OwnsOne(i => i.TotalAmount, moneyBuilder =>
    {
      moneyBuilder.Property(m => m.Amount).HasColumnType("decimal(18,2)").HasColumnName("TotalAmount");
      moneyBuilder.Property(m => m.Currency).HasColumnName("TotalAmountCurrency");
    });
    builder.OwnsOne(i => i.PaidAmount, moneyBuilder =>
    {
      moneyBuilder.Property(m => m.Amount).HasColumnType("decimal(18,2)").HasColumnName("PaidAmount");
      moneyBuilder.Property(m => m.Currency).HasColumnName("PaidAmountCurrency");
    });


    builder.HasOne(i => i.Job).WithOne(j => j.Invoice).HasForeignKey<Invoice>(i => i.JobId);
    builder.HasOne(i => i.Customer).WithMany(c => c.Invoices).HasForeignKey(i => i.CustomerId);
    builder.HasMany(i => i.Items).WithOne(item => item.Invoice).HasForeignKey(item => item.InvoiceId);
  }
}
