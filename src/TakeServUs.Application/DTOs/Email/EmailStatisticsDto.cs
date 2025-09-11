namespace TakeServUs.Application.DTOs.Email;

public class EmailStatisticsDto
{
  public DateTime FromDate { get; set; }
  public DateTime ToDate { get; set; }

  // Volume statistics
  public int TotalEmailsSent { get; set; }
  public int TotalEmailsFailed { get; set; }
  public int TotalEmailsPending { get; set; }
  public int UniqueRecipients { get; set; }

  // Success metrics
  public double DeliveryRate { get; set; }
  public double FailureRate { get; set; }
  public double RetryRate { get; set; }

  // Performance metrics
  public double AverageDeliveryTimeSeconds { get; set; }
  public double MedianDeliveryTimeSeconds { get; set; }
  public double MaxDeliveryTimeSeconds { get; set; }

  // Breakdown by type
  public List<EmailTypeStatisticsDto> ByType { get; set; } = new();

  // Breakdown by template
  public List<EmailTemplateStatisticsDto> ByTemplate { get; set; } = new();

  // Time-based breakdown
  public List<DailyEmailStatisticsDto> DailyBreakdown { get; set; } = new();
  public List<HourlyEmailStatisticsDto> HourlyPattern { get; set; } = new();

  // Top recipients
  public List<TopRecipientDto> TopRecipients { get; set; } = new();

  // Failure analysis
  public List<FailureReasonDto> FailureReasons { get; set; } = new();
}

public class EmailTypeStatisticsDto
{
  public string Type { get; set; } = string.Empty; // Invoice, JobAssignment, Welcome, etc.
  public int Count { get; set; }
  public double SuccessRate { get; set; }
  public double AverageDeliveryTimeSeconds { get; set; }
}

public class EmailTemplateStatisticsDto
{
  public string TemplateName { get; set; } = string.Empty;
  public int UseCount { get; set; }
  public double SuccessRate { get; set; }
  public DateTime? LastUsed { get; set; }
}

public class DailyEmailStatisticsDto
{
  public DateTime Date { get; set; }
  public int Sent { get; set; }
  public int Failed { get; set; }
  public double SuccessRate { get; set; }
}

public class HourlyEmailStatisticsDto
{
  public int Hour { get; set; } // 0-23
  public int AverageCount { get; set; }
  public double AverageSuccessRate { get; set; }
}

public class TopRecipientDto
{
  public string Email { get; set; } = string.Empty;
  public int EmailCount { get; set; }
  public DateTime LastEmailDate { get; set; }
}

public class FailureReasonDto
{
  public string Reason { get; set; } = string.Empty;
  public int Count { get; set; }
  public double Percentage { get; set; }
}
