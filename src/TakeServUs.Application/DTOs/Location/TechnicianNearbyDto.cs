namespace TakeServUs.Application.DTOs.Location;

public class TechnicianNearbyDto
{
  public Guid TechnicianId { get; set; }
  public string TechnicianName { get; set; } = string.Empty;
  public string? TechnicianPhone { get; set; }
  public string? TechnicianEmail { get; set; }
  public bool IsAvailable { get; set; }
  public decimal Latitude { get; set; }
  public decimal Longitude { get; set; }
  public DateTime LastLocationUpdate { get; set; }
  public double DistanceKm { get; set; }
  public int EstimatedArrivalMinutes { get; set; }
  public string? CurrentActivityType { get; set; }
  public double? CurrentSpeed { get; set; }
  public int ActiveJobs { get; set; }
  public int CompletedJobsToday { get; set; }
  public double? Rating { get; set; }
  public List<string> Skills { get; set; } = new();
}
