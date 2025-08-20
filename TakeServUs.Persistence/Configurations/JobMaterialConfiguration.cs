using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Infrastructure.Persistence.Configurations;

public class JobMaterialConfiguration : IEntityTypeConfiguration<JobMaterial>
{
  public void Configure(EntityTypeBuilder<JobMaterial> builder)
  {
    builder.ToTable("JobMaterials");
    builder.HasKey(jm => jm.Id);

    builder.Property(jm => jm.Quantity).HasColumnType("decimal(18,2)");

    builder.OwnsOne(jm => jm.UnitCost, moneyBuilder =>
   {
     moneyBuilder.Property(m => m.Amount).HasColumnType("decimal(18,2)").HasColumnName("UnitCost");
     moneyBuilder.Property(m => m.Currency).HasColumnName("UnitCostCurrency");
   });
    builder.OwnsOne(jm => jm.UnitPrice, moneyBuilder =>
    {
      moneyBuilder.Property(m => m.Amount).HasColumnType("decimal(18,2)").HasColumnName("UnitPrice");
      moneyBuilder.Property(m => m.Currency).HasColumnName("UnitPriceCurrency");
    });
    builder.OwnsOne(jm => jm.TotalCost, moneyBuilder =>
    {
      moneyBuilder.Property(m => m.Amount).HasColumnType("decimal(18,2)").HasColumnName("TotalCost");
      moneyBuilder.Property(m => m.Currency).HasColumnName("TotalCostCurrency");
    });
    builder.OwnsOne(jm => jm.TotalPrice, moneyBuilder =>
    {
      moneyBuilder.Property(m => m.Amount).HasColumnType("decimal(18,2)").HasColumnName("TotalPrice");
      moneyBuilder.Property(m => m.Currency).HasColumnName("TotalPriceCurrency");
    });


    builder.HasOne(jm => jm.Job).WithMany(j => j.JobMaterials).HasForeignKey(jm => jm.JobId);
    builder.HasOne(jm => jm.Material).WithMany(m => m.JobMaterials).HasForeignKey(jm => jm.MaterialId);
    builder.HasOne(jm => jm.UsedByTechnician).WithMany().HasForeignKey(jm => jm.UsedByTechnicianId);
  }
}
