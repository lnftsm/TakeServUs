namespace TakeServUs.Application.DTOs.Feedback;

public class FeedbackStatisticsDto
{
  public int TotalFeedbacks { get; set; }
  public double AverageOverallRating { get; set; }
  public double? AverageQualityRating { get; set; }
  public double? AveragePunctualityRating { get; set; }
  public double? AverageProfessionalismRating { get; set; }
  public double? AverageValueRating { get; set; }

  // Rating distribution (1-5 stars)
  public Dictionary<int, int> RatingDistribution { get; set; } = new();

  // Percentage breakdown
  public double PositivePercentage { get; set; } // 4-5 stars
  public double NeutralPercentage { get; set; } // 3 stars
  public double NegativePercentage { get; set; } // 1-2 stars

  // Response metrics
  public int TotalWithComments { get; set; }
  public int TotalWithResponses { get; set; }
  public double ResponseRate { get; set; }

  // Time-based metrics
  public int FeedbacksThisMonth { get; set; }
  public int FeedbacksLastMonth { get; set; }
  public double MonthOverMonthGrowth { get; set; }

  // Top performers (for company-wide statistics)
  public List<TopPerformerDto>? TopTechnicians { get; set; }

  // Common themes (extracted from comments)
  public List<string>? CommonPositiveThemes { get; set; }
  public List<string>? CommonNegativeThemes { get; set; }
}

public class TopPerformerDto
{
  public Guid TechnicianId { get; set; }
  public string TechnicianName { get; set; } = string.Empty;
  public double AverageRating { get; set; }
  public int TotalFeedbacks { get; set; }
}
