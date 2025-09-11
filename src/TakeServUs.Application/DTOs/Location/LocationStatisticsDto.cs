namespace TakeServUs.Application.DTOs.Location;

public class LocationStatisticsDto
{
  public Guid TechnicianId { get; set; }
  public string TechnicianName { get; set; } = string.Empty;
  public DateTime FromDate { get; set; }
  public DateTime ToDate { get; set; }

  // Distance metrics
  public double TotalDistanceKm { get; set; }
  public double AverageDistancePerDayKm { get; set; }
  public double MaxDistanceInOneDayKm { get; set; }
  public DateTime? MaxDistanceDate { get; set; }

  // Time metrics
  public TimeSpan TotalDrivingTime { get; set; }
  public TimeSpan TotalWorkingTime { get; set; }
  public TimeSpan AverageDrivingTimePerDay { get; set; }
  public TimeSpan AverageWorkingTimePerDay { get; set; }

  // Speed metrics
  public double AverageSpeedKmh { get; set; }
  public double MaxSpeedKmh { get; set; }
  public DateTime? MaxSpeedTimestamp { get; set; }

  // Efficiency metrics
  public int TotalJobsCompleted { get; set; }
  public double AverageJobsPerDay { get; set; }
  public double AverageDistancePerJobKm { get; set; }
  public TimeSpan AverageTimePerJob { get; set; }
  public TimeSpan AverageResponseTime { get; set; } // Time from assignment to arrival

  // Coverage metrics
  public List<AreaCoverageDto> AreasCovered { get; set; } = new();
  public int UniqueLocationsVisited { get; set; }

  // Daily breakdown
  public List<DailyStatisticsDto> DailyStatistics { get; set; } = new();
}

public class AreaCoverageDto
{
  public string City { get; set; } = string.Empty;
  public string State { get; set; } = string.Empty;
  public int VisitCount { get; set; }
  public double TotalTimeHours { get; set; }
}

public class DailyStatisticsDto
{
  public DateTime Date { get; set; }
  public double DistanceKm { get; set; }
  public TimeSpan DrivingTime { get; set; }
  public int JobsCompleted { get; set; }
  public double AverageSpeedKmh { get; set; }
}
