using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Persistence.Configurations;

public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
{
  public void Configure(EntityTypeBuilder<Quote> builder)
  {
    builder.ToTable("quotes");

    builder.HasKey(q => q.Id);

    // QuoteNumber VO'sunu tek bir kolona dönüştürme
    builder.Property(q => q.QuoteNumber)
        .IsRequired()
        .HasMaxLength(50);

    // Money VO'larını owned type olarak yapılandırma
    builder.OwnsOne(q => q.SubTotal, sb =>
    {
      sb.Property(m => m.Amount).HasColumnName("sub_total_amount").HasColumnType("decimal(18,2)");
      sb.Property(m => m.Currency).HasColumnName("sub_total_currency");
    });

    builder.OwnsOne(q => q.TaxAmount, sb =>
    {
      sb.Property(m => m.Amount).HasColumnName("tax_amount").HasColumnType("decimal(18,2)");
      sb.Property(m => m.Currency).HasColumnName("tax_currency");
    });

    builder.OwnsOne(q => q.DiscountAmount, sb =>
    {
      sb.Property(m => m.Amount).HasColumnName("discount_amount").HasColumnType("decimal(18,2)");
      sb.Property(m => m.Currency).HasColumnName("discount_currency");
    });

    builder.OwnsOne(q => q.TotalAmount, sb =>
    {
      sb.Property(m => m.Amount).HasColumnName("total_amount").HasColumnType("decimal(18,2)");
      sb.Property(m => m.Currency).HasColumnName("total_currency");
    });

    // Soft Delete
    builder.HasQueryFilter(q => !q.IsDeleted);

    // İlişkiler
    builder.HasOne(q => q.Job)
        .WithOne(j => j.Quote)
        .HasForeignKey<Quote>(q => q.JobId)
        .OnDelete(DeleteBehavior.Restrict);

    builder.HasMany(q => q.Items)
        .WithOne(it => it.Quote)
        .HasForeignKey(it => it.QuoteId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
