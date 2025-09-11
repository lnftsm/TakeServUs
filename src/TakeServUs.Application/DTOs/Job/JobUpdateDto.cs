using System.ComponentModel.DataAnnotations;
using TakeServUs.Domain.Enums;

namespace TakeServUs.Application.DTOs.Job;

public class JobUpdateDto
{
  [MaxLength(250, ErrorMessage = "Title cannot exceed 250 characters")]
  public string? Title { get; set; }

  [MaxLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
  public string? Description { get; set; }

  public JobPriority? Priority { get; set; }

  // Service location (all optional for partial updates)
  [MaxLength(255, ErrorMessage = "Street address cannot exceed 255 characters")]
  public string? ServiceStreet { get; set; }

  [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters")]
  public string? ServiceCity { get; set; }

  [MaxLength(100, ErrorMessage = "State cannot exceed 100 characters")]
  public string? ServiceState { get; set; }

  [MaxLength(20, ErrorMessage = "Postal code cannot exceed 20 characters")]
  public string? ServicePostalCode { get; set; }

  [MaxLength(100, ErrorMessage = "Country cannot exceed 100 characters")]
  public string? ServiceCountry { get; set; }

  public decimal? ServiceLatitude { get; set; }
  public decimal? ServiceLongitude { get; set; }

  // Scheduling
  public DateTime? ScheduledStartTime { get; set; }
  public DateTime? ScheduledEndTime { get; set; }

  [Range(1, 10080, ErrorMessage = "Estimated duration must be between 1 minute and 1 week")]
  public int? EstimatedDurationMinutes { get; set; }

  // Pricing
  [Range(0, double.MaxValue, ErrorMessage = "Estimated cost must be non-negative")]
  public decimal? EstimatedCost { get; set; }

  [Range(0, double.MaxValue, ErrorMessage = "Actual cost must be non-negative")]
  public decimal? ActualCost { get; set; }

  // Additional information
  [MaxLength(2000, ErrorMessage = "Notes cannot exceed 2000 characters")]
  public string? Notes { get; set; }

  [MaxLength(2000, ErrorMessage = "Internal notes cannot exceed 2000 characters")]
  public string? InternalNotes { get; set; }

  public bool? RequiresCustomerSignature { get; set; }

  // Recurrence
  public bool? IsRecurring { get; set; }
  public RecurrencePatternDto? RecurrencePattern { get; set; }
}

// Supporting DTOs

public class JobAssignmentDto
{
  public Guid Id { get; set; }
  public Guid JobId { get; set; }
  public Guid TechnicianId { get; set; }
  public string TechnicianName { get; set; } = string.Empty;
  public DateTime AssignedAt { get; set; }
  public DateTime? AcceptedAt { get; set; }
  public DateTime? DeclinedAt { get; set; }
  public DateTime? CompletedAt { get; set; }
  public bool IsPrimary { get; set; }
  public string? Notes { get; set; }
  public string Status { get; set; } = string.Empty; // Pending, Accepted, Declined, Completed
}

public class JobMaterialDto
{
  public Guid Id { get; set; }
  public Guid MaterialId { get; set; }
  public string MaterialCode { get; set; } = string.Empty;
  public string MaterialName { get; set; } = string.Empty;
  public decimal Quantity { get; set; }
  public string Unit { get; set; } = string.Empty;
  public decimal? UnitCost { get; set; }
  public decimal? UnitPrice { get; set; }
  public decimal? TotalCost { get; set; }
  public decimal? TotalPrice { get; set; }
  public DateTime UsedAt { get; set; }
  public string? UsedByTechnicianName { get; set; }
  public string? Notes { get; set; }
}

public class JobStatusHistoryDto
{
  public Guid Id { get; set; }
  public JobStatusType Status { get; set; }
  public DateTime ChangedAt { get; set; }
  public string? ChangedBy { get; set; }
  public string? Notes { get; set; }
  public decimal? Latitude { get; set; }
  public decimal? Longitude { get; set; }
}

public class RecurrencePatternDto
{
  public string Type { get; set; } = "Daily"; // Daily, Weekly, Monthly, Yearly
  public int Interval { get; set; } = 1; // Every N days/weeks/months
  public List<DayOfWeek>? DaysOfWeek { get; set; } // For weekly recurrence
  public int? DayOfMonth { get; set; } // For monthly recurrence
  public DateTime? EndDate { get; set; }
  public int? Occurrences { get; set; } // Number of occurrences
}
