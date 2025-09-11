using System.ComponentModel.DataAnnotations;

namespace TakeServUs.Application.DTOs.Email;

public class EmailQueueDto
{
  [Required(ErrorMessage = "Recipient email is required")]
  [EmailAddress(ErrorMessage = "Invalid email format")]
  public string To { get; set; } = string.Empty;

  [EmailAddress(ErrorMessage = "Invalid email format")]
  public string From { get; set; } = "noreply@takeservus.com";

  [Required(ErrorMessage = "Subject is required")]
  [MaxLength(500, ErrorMessage = "Subject cannot exceed 500 characters")]
  public string Subject { get; set; } = string.Empty;

  [Required(ErrorMessage = "Email body is required")]
  public string Body { get; set; } = string.Empty;

  // Optional CC and BCC
  public List<string>? Cc { get; set; }
  public List<string>? Bcc { get; set; }

  // Email metadata
  public string? ReplyTo { get; set; }
  public Dictionary<string, string>? Headers { get; set; }
  public List<EmailAttachmentDto>? Attachments { get; set; }

  // Tracking
  public string? ReferenceId { get; set; } // External reference (e.g., JobId, InvoiceId)
  public string? ReferenceType { get; set; } // Type of reference (Job, Invoice, etc.)

  // Priority and scheduling
  public int Priority { get; set; } = 5; // 1-10, 1 being highest
  public DateTime? ScheduledFor { get; set; }

  // Template
  public bool IsHtml { get; set; } = true;
  public string? TemplateName { get; set; }
  public Dictionary<string, string>? TemplateVariables { get; set; }
}

public class EmailAttachmentDto
{
  public string FileName { get; set; } = string.Empty;
  public string ContentType { get; set; } = string.Empty;
  public byte[] Content { get; set; } = Array.Empty<byte>();
  public bool IsInline { get; set; } = false;
  public string? ContentId { get; set; } // For inline attachments
}
