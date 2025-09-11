namespace TakeServUs.Application.DTOs.Email;

public class EmailQueueStatusDto
{
  public int TotalQueued { get; set; }
  public int PendingEmails { get; set; }
  public int ProcessingEmails { get; set; }
  public int FailedEmails { get; set; }
  public int SentToday { get; set; }
  public int SentThisHour { get; set; }

  // Performance metrics
  public double AverageProcessingTimeSeconds { get; set; }
  public double SuccessRate { get; set; }
  public double FailureRate { get; set; }

  // Queue health
  public string QueueHealth { get; set; } = "Healthy"; // Healthy, Degraded, Critical
  public List<string> HealthIssues { get; set; } = new();

  // Next processing
  public DateTime? NextProcessingTime { get; set; }
  public bool IsProcessing { get; set; }

  // Breakdown by priority
  public Dictionary<int, int> QueueByPriority { get; set; } = new();

  // Recent activity
  public List<EmailProcessingEventDto> RecentEvents { get; set; } = new();
}

public class EmailProcessingEventDto
{
  public DateTime Timestamp { get; set; }
  public string EventType { get; set; } = string.Empty; // Sent, Failed, Retrying
  public string EmailSubject { get; set; } = string.Empty;
  public string Recipient { get; set; } = string.Empty;
  public string? ErrorMessage { get; set; }
}
