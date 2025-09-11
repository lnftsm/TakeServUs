using System.ComponentModel.DataAnnotations;

namespace TakeServUs.Application.DTOs.Technician;

public class TechnicianLocationDto
{
  public Guid TechnicianId { get; set; }
  public string TechnicianName { get; set; } = string.Empty;

  [Required(ErrorMessage = "Latitude is required")]
  [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
  public decimal Latitude { get; set; }

  [Required(ErrorMessage = "Longitude is required")]
  [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
  public decimal Longitude { get; set; }

  public DateTime Timestamp { get; set; }

  [Range(0, double.MaxValue, ErrorMessage = "Accuracy must be non-negative")]
  public decimal? Accuracy { get; set; }

  [Range(0, 500, ErrorMessage = "Speed must be between 0 and 500 km/h")]
  public double? Speed { get; set; }

  [Range(0, 360, ErrorMessage = "Heading must be between 0 and 360 degrees")]
  public double? Heading { get; set; }

  public string? ActivityType { get; set; }

  // Current job information
  public Guid? CurrentJobId { get; set; }
  public string? CurrentJobNumber { get; set; }
  public string? CurrentJobAddress { get; set; }
  public double? DistanceToCurrentJob { get; set; }
  public int? EstimatedArrivalMinutes { get; set; }

  // Status
  public bool IsOnline { get; set; }
  public bool IsAvailable { get; set; }
  public int BatteryLevel { get; set; }
}
