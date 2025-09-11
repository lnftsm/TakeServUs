using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Persistence.Configurations;

public class MaterialConfiguration : IEntityTypeConfiguration<Material>
{
  public void Configure(EntityTypeBuilder<Material> builder)
  {
    builder.ToTable("materials");

    builder.HasKey(m => m.Id);

    // Bir şirket içindeki malzeme kodunun benzersiz olmasını sağlar
    builder.HasIndex(m => new { m.CompanyId, m.Code }).IsUnique();

    builder.Property(m => m.Code)
        .IsRequired()
        .HasMaxLength(50);

    builder.Property(m => m.Name)
        .IsRequired()
        .HasMaxLength(200);

    builder.Property(m => m.Unit)
        .IsRequired()
        .HasMaxLength(20);

    builder.Property(m => m.QuantityInStock)
        .HasColumnType("decimal(18,3)")
        .IsRequired();

    // Money VO'larını yapılandırma
    builder.OwnsOne(m => m.UnitCost, sb =>
    {
      sb.Property(p => p.Amount).HasColumnName("unit_cost_amount").HasColumnType("decimal(18,2)");
      sb.Property(p => p.Currency).HasColumnName("unit_cost_currency");
    });

    builder.OwnsOne(m => m.UnitPrice, sb =>
    {
      sb.Property(p => p.Amount).HasColumnName("unit_price_amount").HasColumnType("decimal(18,2)");
      sb.Property(p => p.Currency).HasColumnName("unit_price_currency");
    });

    // Soft Delete
    builder.HasQueryFilter(m => !m.IsDeleted);

    // İlişkiler
    builder.HasOne(m => m.Company)
        .WithMany(c => c.Materials)
        .HasForeignKey(m => m.CompanyId)
        .OnDelete(DeleteBehavior.Restrict);
  }
}
