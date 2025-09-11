using System.ComponentModel.DataAnnotations;
using TakeServUs.Domain.Enums;

namespace TakeServUs.Application.DTOs.Notification;

public class NotificationCreateDto
{
  [Required(ErrorMessage = "User ID is required")]
  public Guid UserId { get; set; }

  [Required(ErrorMessage = "Notification type is required")]
  public NotificationType Type { get; set; }

  [Required(ErrorMessage = "Title is required")]
  [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
  public string Title { get; set; } = string.Empty;

  [Required(ErrorMessage = "Message is required")]
  [MaxLength(1000, ErrorMessage = "Message cannot exceed 1000 characters")]
  public string Message { get; set; } = string.Empty;

  // Additional data as key-value pairs
  public Dictionary<string, object>? Data { get; set; }

  // Channel selection
  public NotificationChannel Channel { get; set; } = NotificationChannel.InAppNotification;

  // Options
  public bool SendImmediately { get; set; } = true;
  public DateTime? ScheduledFor { get; set; }

  // For bulk notifications
  public List<Guid>? UserIds { get; set; }

  // Template-based creation
  public string? TemplateName { get; set; }
  public Dictionary<string, string>? TemplateVariables { get; set; }
}

// Supporting DTOs for different notification scenarios

public class JobNotificationDto
{
  public Guid JobId { get; set; }
  public string JobNumber { get; set; } = string.Empty;
  public string JobTitle { get; set; } = string.Empty;
  public string CustomerName { get; set; } = string.Empty;
  public string? TechnicianName { get; set; }
  public JobStatusType? Status { get; set; }
  public DateTime? ScheduledTime { get; set; }
}

public class InvoiceNotificationDto
{
  public Guid InvoiceId { get; set; }
  public string InvoiceNumber { get; set; } = string.Empty;
  public decimal Amount { get; set; }
  public string Currency { get; set; } = "TRY";
  public DateTime DueDate { get; set; }
  public PaymentStatus? PaymentStatus { get; set; }
}

public class SystemNotificationDto
{
  public string AlertType { get; set; } = string.Empty; // Maintenance, Update, Warning, Error
  public string Severity { get; set; } = "Info"; // Info, Warning, Error, Critical
  public DateTime? StartTime { get; set; }
  public DateTime? EndTime { get; set; }
  public string? AffectedServices { get; set; }
  public string? ActionRequired { get; set; }
}
