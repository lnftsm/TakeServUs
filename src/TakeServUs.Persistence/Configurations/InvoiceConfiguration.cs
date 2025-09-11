using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;
using TakeServUs.Domain.Enums;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Persistence.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
  public void Configure(EntityTypeBuilder<Invoice> builder)
  {
    builder.ToTable("invoices");

    builder.HasKey(i => i.Id);

    // InvoiceNumber VO'sunu tek bir kolona dönüştürme
    builder.Property(i => i.InvoiceNumber)
        .IsRequired()
        .HasMaxLength(50)
        .HasConversion(
            v => v.Value,
            v => new InvoiceNumber(v));

    // Money VO'larını owned type olarak yapılandırma
    // Her bir owned type için kolon isimlerini belirtmek önemlidir
    builder.OwnsOne(i => i.SubTotal, sb =>
    {
      sb.Property(m => m.Amount).HasColumnName("sub_total_amount").HasColumnType("decimal(18,2)");
      sb.Property(m => m.Currency).HasColumnName("sub_total_currency");
    });

    builder.OwnsOne(i => i.TaxAmount, sb =>
    {
      sb.Property(m => m.Amount).HasColumnName("tax_amount").HasColumnType("decimal(18,2)");
      sb.Property(m => m.Currency).HasColumnName("tax_currency");
    });

    builder.OwnsOne(i => i.DiscountAmount, sb =>
    {
      sb.Property(m => m.Amount).HasColumnName("discount_amount").HasColumnType("decimal(18,2)");
      sb.Property(m => m.Currency).HasColumnName("discount_currency");
    });

    builder.OwnsOne(i => i.TotalAmount, sb =>
    {
      sb.Property(m => m.Amount).HasColumnName("total_amount").HasColumnType("decimal(18,2)");
      sb.Property(m => m.Currency).HasColumnName("total_currency");
    });

    builder.OwnsOne(i => i.PaidAmount, sb =>
    {
      sb.Property(m => m.Amount).HasColumnName("paid_amount").HasColumnType("decimal(18,2)");
      sb.Property(m => m.Currency).HasColumnName("paid_currency");
    });

    builder.Property(i => i.PaymentStatus).HasDefaultValue(PaymentStatus.Pending);

    // Soft Delete
    builder.HasQueryFilter(i => !i.IsDeleted);

    // İlişkiler
    builder.HasOne(i => i.Job)
        .WithOne(j => j.Invoice)
        .HasForeignKey<Invoice>(i => i.JobId)
        .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(i => i.Customer)
        .WithMany(c => c.Invoices)
        .HasForeignKey(i => i.CustomerId)
        .OnDelete(DeleteBehavior.Restrict);

    builder.HasMany(i => i.Items)
        .WithOne(it => it.Invoice)
        .HasForeignKey(it => it.InvoiceId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
