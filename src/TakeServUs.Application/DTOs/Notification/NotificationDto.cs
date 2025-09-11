using TakeServUs.Application.Common.Mappings;
using TakeServUs.Domain.Enums;
using AutoMapper;
using System.Text.Json;

namespace TakeServUs.Application.DTOs.Notification;

public class NotificationDto : IMapFrom<Domain.Entities.Notification>
{
  public Guid Id { get; set; }
  public Guid UserId { get; set; }
  public string UserName { get; set; } = string.Empty;
  public string UserEmail { get; set; } = string.Empty;
  public NotificationType Type { get; set; }
  public string Title { get; set; } = string.Empty;
  public string Message { get; set; } = string.Empty;
  public Dictionary<string, object>? Data { get; set; }
  public bool IsRead { get; set; }
  public DateTime? ReadAt { get; set; }
  public bool IsSent { get; set; }
  public DateTime? SentAt { get; set; }
  public NotificationChannel Channel { get; set; }
  public DateTime CreatedAt { get; set; }

  // Calculated fields
  public string TimeAgo { get; set; } = string.Empty;
  public string TypeDisplayName { get; set; } = string.Empty;
  public string IconName { get; set; } = string.Empty;
  public string Priority { get; set; } = "Normal"; // Low, Normal, High, Urgent

  // Action fields
  public string? ActionUrl { get; set; }
  public string? ActionText { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Domain.Entities.Notification, NotificationDto>()
        .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.User.FullName.GetFullName()))
        .ForMember(d => d.UserEmail, opt => opt.MapFrom(s => s.User.Email.Value))
        .ForMember(d => d.Data, opt => opt.MapFrom(s => string.IsNullOrEmpty(s.Data) ? null : JsonSerializer.Deserialize<Dictionary<string, object>>(s.Data, new JsonSerializerOptions())))
        .ForMember(d => d.TimeAgo, opt => opt.MapFrom(s => GetTimeAgo(s.CreatedAt)))
        .ForMember(d => d.TypeDisplayName, opt => opt.MapFrom(s => GetTypeDisplayName(s.Type)))
        .ForMember(d => d.IconName, opt => opt.MapFrom(s => GetIconName(s.Type)))
        .ForMember(d => d.Priority, opt => opt.MapFrom(s => GetPriority(s.Type)))
        .ForMember(d => d.ActionUrl, opt => opt.MapFrom(s => GetActionUrl(s.Type, s.Data)))
        .ForMember(d => d.ActionText, opt => opt.MapFrom(s => GetActionText(s.Type)));
  }

  private static string GetTimeAgo(DateTime createdAt)
  {
    var timeSpan = DateTime.UtcNow - createdAt;

    return timeSpan switch
    {
      { TotalSeconds: < 60 } => "just now",
      { TotalMinutes: < 60 } => $"{(int)timeSpan.TotalMinutes} minute{((int)timeSpan.TotalMinutes == 1 ? "" : "s")} ago",
      { TotalHours: < 24 } => $"{(int)timeSpan.TotalHours} hour{((int)timeSpan.TotalHours == 1 ? "" : "s")} ago",
      { TotalDays: < 7 } => $"{(int)timeSpan.TotalDays} day{((int)timeSpan.TotalDays == 1 ? "" : "s")} ago",
      { TotalDays: < 30 } => $"{(int)(timeSpan.TotalDays / 7)} week{((int)(timeSpan.TotalDays / 7) == 1 ? "" : "s")} ago",
      { TotalDays: < 365 } => $"{(int)(timeSpan.TotalDays / 30)} month{((int)(timeSpan.TotalDays / 30) == 1 ? "" : "s")} ago",
      _ => $"{(int)(timeSpan.TotalDays / 365)} year{((int)(timeSpan.TotalDays / 365) == 1 ? "" : "s")} ago"
    };
  }

  private static string GetTypeDisplayName(NotificationType type)
  {
    return type switch
    {
      NotificationType.JobCreated => "New Job",
      NotificationType.JobAssigned => "Job Assignment",
      NotificationType.JobStatusChanged => "Job Status Update",
      NotificationType.TechnicianEnRoute => "Technician En Route",
      NotificationType.TechnicianArrived => "Technician Arrived",
      NotificationType.JobCompleted => "Job Completed",
      NotificationType.InvoiceCreated => "New Invoice",
      NotificationType.PaymentReceived => "Payment Received",
      NotificationType.FeedbackRequested => "Feedback Request",
      NotificationType.SystemAlert => "System Alert",
      _ => type.ToString()
    };
  }

  private static string GetIconName(NotificationType type)
  {
    return type switch
    {
      NotificationType.JobCreated => "briefcase",
      NotificationType.JobAssigned => "user-check",
      NotificationType.JobStatusChanged => "refresh-cw",
      NotificationType.TechnicianEnRoute => "navigation",
      NotificationType.TechnicianArrived => "map-pin",
      NotificationType.JobCompleted => "check-circle",
      NotificationType.InvoiceCreated => "file-text",
      NotificationType.PaymentReceived => "dollar-sign",
      NotificationType.FeedbackRequested => "star",
      NotificationType.SystemAlert => "alert-triangle",
      _ => "bell"
    };
  }

  private static string GetPriority(NotificationType type)
  {
    return type switch
    {
      NotificationType.SystemAlert => "Urgent",
      NotificationType.JobAssigned => "High",
      NotificationType.TechnicianEnRoute => "High",
      NotificationType.PaymentReceived => "High",
      NotificationType.JobCompleted => "Normal",
      NotificationType.InvoiceCreated => "Normal",
      NotificationType.FeedbackRequested => "Low",
      _ => "Normal"
    };
  }

  private static string? GetActionUrl(NotificationType type, string? data)
  {
    if (string.IsNullOrEmpty(data))
      return null;

    try
    {
      var dataDict = JsonSerializer.Deserialize<Dictionary<string, object>>(data, new JsonSerializerOptions());

      return type switch
      {
        NotificationType.JobCreated or NotificationType.JobAssigned or NotificationType.JobStatusChanged
            or NotificationType.TechnicianEnRoute or NotificationType.TechnicianArrived or NotificationType.JobCompleted
            => dataDict?.ContainsKey("jobId") == true ? $"/jobs/{dataDict["jobId"]}" : null,

        NotificationType.InvoiceCreated or NotificationType.PaymentReceived
            => dataDict?.ContainsKey("invoiceId") == true ? $"/invoices/{dataDict["invoiceId"]}" : null,

        NotificationType.FeedbackRequested
            => dataDict?.ContainsKey("jobId") == true ? $"/feedback/create?jobId={dataDict["jobId"]}" : null,

        _ => null
      };
    }
    catch
    {
      return null;
    }
  }

  private static string? GetActionText(NotificationType type)
  {
    return type switch
    {
      NotificationType.JobCreated or NotificationType.JobAssigned or NotificationType.JobStatusChanged
          or NotificationType.TechnicianEnRoute or NotificationType.TechnicianArrived or NotificationType.JobCompleted
          => "View Job",

      NotificationType.InvoiceCreated or NotificationType.PaymentReceived
          => "View Invoice",

      NotificationType.FeedbackRequested
          => "Leave Feedback",

      NotificationType.SystemAlert
          => "View Details",

      _ => null
    };
  }
}
