using TakeServUs.Domain.Common;

namespace TakeServUs.Domain.Entities
{
  public class Notification : AuditableEntity
  {
    public Guid UserId { get; set; }
    public NotificationType Type { get; set; }
    public required string Title { get; set; }
    public required string Message { get; set; }
    public string? Data { get; set; } // JSON data for additional info
    public bool IsRead { get; set; } = false;
    public DateTime? ReadAt { get; set; }
    public bool IsSent { get; set; } = false;
    public DateTime? SentAt { get; set; }
    public string? Channel { get; set; } // Email, SMS, Push, InApp

    // Relations
    public virtual User User { get; set; } = default!;
  }
}

public enum NotificationType
{
  JobCreated = 1,
  JobAssigned = 2,
  JobStatusChanged = 3,
  TechnicianEnRoute = 4,
  TechnicianArrived = 5,
  JobCompleted = 6,
  InvoiceCreated = 7,
  PaymentReceived = 8,
  FeedbackRequested = 9,
  SystemAlert = 10
}

