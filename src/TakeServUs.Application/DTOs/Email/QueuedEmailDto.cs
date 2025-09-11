using TakeServUs.Application.Common.Mappings;
using AutoMapper;

namespace TakeServUs.Application.DTOs.Email;

public class QueuedEmailDto : IMapFrom<Domain.Entities.QueuedEmail>
{
  public Guid Id { get; set; }
  public string From { get; set; } = string.Empty;
  public string To { get; set; } = string.Empty;
  public string Subject { get; set; } = string.Empty;
  public string Body { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; }
  public DateTime? SentAt { get; set; }
  public int SentTries { get; set; }
  public bool IsSent { get; set; }

  // Additional display fields
  public string Status { get; set; } = string.Empty;
  public string? ErrorMessage { get; set; }
  public DateTime? NextRetryAt { get; set; }
  public string BodyPreview { get; set; } = string.Empty;

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Domain.Entities.QueuedEmail, QueuedEmailDto>()
        .ForMember(d => d.Status, opt => opt.MapFrom(s => GetStatus(s)))
        .ForMember(d => d.NextRetryAt, opt => opt.MapFrom(s => GetNextRetryTime(s)))
        .ForMember(d => d.BodyPreview, opt => opt.MapFrom(s => GetBodyPreview(s.Body)));
  }

  private static string GetStatus(Domain.Entities.QueuedEmail email)
  {
    if (email.IsSent) return "Sent";
    if (email.SentTries == 0) return "Pending";
    if (email.SentTries < 3) return "Retrying";
    return "Failed";
  }

  private static DateTime? GetNextRetryTime(Domain.Entities.QueuedEmail email)
  {
    if (email.IsSent || email.SentTries >= 3) return null;

    // Exponential backoff: 1 min, 5 min, 15 min
    var delayMinutes = email.SentTries switch
    {
      0 => 0,
      1 => 1,
      2 => 5,
      _ => 15
    };

    return email.CreatedAt.AddMinutes(delayMinutes);
  }

  private static string GetBodyPreview(string body)
  {
    if (string.IsNullOrEmpty(body)) return string.Empty;

    // Remove HTML tags for preview
    var text = System.Text.RegularExpressions.Regex.Replace(body, "<.*?>", "");
    return text.Length > 150 ? text.Substring(0, 150) + "..." : text;
  }
}
