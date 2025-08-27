using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Infrastructure.Persistence.Configurations;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
  public void Configure(EntityTypeBuilder<Feedback> builder)
  {
    builder.ToTable("Feedbacks");
    builder.HasKey(f => f.Id);

    builder.HasOne(f => f.Job).WithOne(j => j.Feedback).HasForeignKey<Feedback>(f => f.JobId);
    builder.HasOne(f => f.Customer).WithMany(c => c.Feedbacks).HasForeignKey(f => f.CustomerId);
  }
}
