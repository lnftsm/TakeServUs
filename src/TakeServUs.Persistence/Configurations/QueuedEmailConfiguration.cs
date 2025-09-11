using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Persistence.Configurations;

public class QueuedEmailConfiguration : IEntityTypeConfiguration<QueuedEmail>
{
  public void Configure(EntityTypeBuilder<QueuedEmail> builder)
  {
    builder.ToTable("queued_emails");

    builder.HasKey(e => e.Id);

    // Arka plan servisinin gönderilecek e-postaları
    // verimli bir şekilde sorgulaması için index
    builder.HasIndex(e => new { e.IsSent, e.SentTries });

    builder.Property(e => e.From)
        .IsRequired()
        .HasMaxLength(256);

    builder.Property(e => e.To)
        .IsRequired()
        .HasMaxLength(256);

    builder.Property(e => e.Subject)
        .IsRequired()
        .HasMaxLength(500);

    builder.Property(e => e.Body)
        .IsRequired();
  }
}
