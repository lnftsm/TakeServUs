using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Persistence.Configurations;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
  public void Configure(EntityTypeBuilder<Feedback> builder)
  {
    builder.ToTable("feedbacks");

    builder.HasKey(f => f.Id);

    builder.Property(f => f.OverallRating).IsRequired();

    // Soft Delete
    builder.HasQueryFilter(f => !f.IsDeleted);

    // İlişkiler
    builder.HasOne(f => f.Job)
        .WithOne(j => j.Feedback)
        .HasForeignKey<Feedback>(f => f.JobId)
        .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(f => f.Customer)
        .WithMany(c => c.Feedbacks)
        .HasForeignKey(f => f.CustomerId)
        .OnDelete(DeleteBehavior.Restrict);
  }
}
