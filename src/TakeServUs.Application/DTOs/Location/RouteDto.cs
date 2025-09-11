namespace TakeServUs.Application.DTOs.Location;

public class RouteDto
{
  public Guid TechnicianId { get; set; }
  public string TechnicianName { get; set; } = string.Empty;
  public DateTime Date { get; set; }
  public List<RoutePointDto> Points { get; set; } = new();
  public List<RouteSegmentDto> Segments { get; set; } = new();
  public double TotalDistanceKm { get; set; }
  public TimeSpan TotalDuration { get; set; }
  public TimeSpan DrivingTime { get; set; }
  public TimeSpan StationaryTime { get; set; }
  public double AverageSpeedKmh { get; set; }
  public double MaxSpeedKmh { get; set; }
  public int NumberOfStops { get; set; }
  public List<JobVisitDto> JobVisits { get; set; } = new();
}

public class RoutePointDto
{
  public decimal Latitude { get; set; }
  public decimal Longitude { get; set; }
  public DateTime Timestamp { get; set; }
  public double? Speed { get; set; }
  public string? ActivityType { get; set; }
}

public class RouteSegmentDto
{
  public RoutePointDto StartPoint { get; set; } = new();
  public RoutePointDto EndPoint { get; set; } = new();
  public double DistanceKm { get; set; }
  public TimeSpan Duration { get; set; }
  public double AverageSpeedKmh { get; set; }
  public string ActivityType { get; set; } = string.Empty;
}

public class JobVisitDto
{
  public Guid JobId { get; set; }
  public string JobNumber { get; set; } = string.Empty;
  public string CustomerName { get; set; } = string.Empty;
  public DateTime ArrivalTime { get; set; }
  public DateTime? DepartureTime { get; set; }
  public TimeSpan? Duration { get; set; }
  public decimal Latitude { get; set; }
  public decimal Longitude { get; set; }
}
