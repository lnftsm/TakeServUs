using TakeServUs.Domain.Common;

namespace TakeServUs.Domain.Entities;

public class QueuedEmail : BaseEntity
{
  public string From { get; set; } = "noreply@takeservus.com";
  public required string To { get; set; }
  public required string Subject { get; set; }
  public required string Body { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? SentAt { get; set; }
  public int SentTries { get; set; } = 0;
  public bool IsSent { get; set; } = false;
}
