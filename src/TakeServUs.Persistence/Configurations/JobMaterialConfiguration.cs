using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Persistence.Configurations;

public class JobMaterialConfiguration : IEntityTypeConfiguration<JobMaterial>
{
  public void Configure(EntityTypeBuilder<JobMaterial> builder)
  {
    builder.ToTable("job_materials");

    builder.HasKey(jm => jm.Id);

    // Bir işte aynı malzemenin birden fazla kez eklenmesini önlemek için
    builder.HasIndex(jm => new { jm.JobId, jm.MaterialId }).IsUnique();

    builder.Property(jm => jm.Quantity)
        .HasColumnType("decimal(18,3)")
        .IsRequired();

    // Money VO'larını yapılandırma
    builder.OwnsOne(jm => jm.UnitCost, sb =>
    {
      sb.Property(p => p.Amount).HasColumnName("unit_cost_amount").HasColumnType("decimal(18,2)");
      sb.Property(p => p.Currency).HasColumnName("unit_cost_currency");
    });

    builder.OwnsOne(jm => jm.UnitPrice, sb =>
    {
      sb.Property(p => p.Amount).HasColumnName("unit_price_amount").HasColumnType("decimal(18,2)");
      sb.Property(p => p.Currency).HasColumnName("unit_price_currency");
    });

    builder.OwnsOne(jm => jm.TotalCost, sb =>
    {
      sb.Property(p => p.Amount).HasColumnName("total_cost_amount").HasColumnType("decimal(18,2)");
      sb.Property(p => p.Currency).HasColumnName("total_cost_currency");
    });

    builder.OwnsOne(jm => jm.TotalPrice, sb =>
    {
      sb.Property(p => p.Amount).HasColumnName("total_price_amount").HasColumnType("decimal(18,2)");
      sb.Property(p => p.Currency).HasColumnName("total_price_currency");
    });

    // İlişkiler
    builder.HasOne(jm => jm.Job)
        .WithMany(j => j.JobMaterials)
        .HasForeignKey(jm => jm.JobId)
        .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(jm => jm.Material)
        .WithMany(m => m.JobMaterials)
        .HasForeignKey(jm => jm.MaterialId)
        .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(jm => jm.UsedByTechnician)
        .WithMany()
        .HasForeignKey(jm => jm.UsedByTechnicianId)
        .IsRequired(false)
        .OnDelete(DeleteBehavior.SetNull);
  }
}
