using System.ComponentModel.DataAnnotations;

namespace TakeServUs.Application.DTOs.Location;

public class LocationCreateDto
{
  [Required(ErrorMessage = "Technician ID is required")]
  public Guid TechnicianId { get; set; }

  [Required(ErrorMessage = "Latitude is required")]
  [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
  public decimal Latitude { get; set; }

  [Required(ErrorMessage = "Longitude is required")]
  [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
  public decimal Longitude { get; set; }

  // Optional - will use current UTC time if not provided
  public DateTime? Timestamp { get; set; }

  [Range(0, double.MaxValue, ErrorMessage = "Accuracy must be non-negative")]
  public decimal? Accuracy { get; set; }

  [Range(0, 500, ErrorMessage = "Speed must be between 0 and 500 km/h")]
  public double? Speed { get; set; }

  [Range(0, 360, ErrorMessage = "Heading must be between 0 and 360 degrees")]
  public double? Heading { get; set; }

  [MaxLength(50, ErrorMessage = "Activity type cannot exceed 50 characters")]
  public string? ActivityType { get; set; }

  // For batch uploads
  public string? BatchId { get; set; }

  // Device information
  public string? DeviceId { get; set; }
  public string? DeviceModel { get; set; }
  public string? OsVersion { get; set; }
  public int? BatteryLevel { get; set; }
}
