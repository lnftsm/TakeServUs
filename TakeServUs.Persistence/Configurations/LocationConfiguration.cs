using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Infrastructure.Persistence.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
  public void Configure(EntityTypeBuilder<Location> builder)
  {
    builder.ToTable("TechnicianLocations");
    builder.HasKey(l => l.Id);

    builder.HasOne(l => l.Technician).WithMany(t => t.LocationHistory).HasForeignKey(l => l.TechnicianId);
  }
}
