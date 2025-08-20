using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Infrastructure.Persistence.Configurations;

public class TechnicianConfiguration : IEntityTypeConfiguration<Technician>
{
  public void Configure(EntityTypeBuilder<Technician> builder)
  {
    builder.ToTable("Technicians");
    builder.HasKey(t => t.Id);

    builder.HasOne(c => c.User).WithOne().HasForeignKey<Customer>(c => c.UserId);
    builder.HasOne(t => t.Company).WithMany(c => c.Technicians).HasForeignKey(t => t.CompanyId);
  }
}
