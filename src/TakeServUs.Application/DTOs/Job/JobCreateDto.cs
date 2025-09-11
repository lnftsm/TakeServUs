using System.ComponentModel.DataAnnotations;
using TakeServUs.Domain.Enums;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Application.DTOs.Job;

public class JobCreateDto
{
  // Optional - will be auto-generated if not provided
  public string? JobNumber { get; set; }

  // Optional - will use current user's company if not provided
  public Guid? CompanyId { get; set; }

  [Required(ErrorMessage = "Customer ID is required")]
  public Guid CustomerId { get; set; }

  [Required(ErrorMessage = "Job title is required")]
  [MaxLength(250, ErrorMessage = "Title cannot exceed 250 characters")]
  public string Title { get; set; } = string.Empty;

  [MaxLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
  public string? Description { get; set; }

  public JobPriority Priority { get; set; } = JobPriority.Normal;

  // Service location
  [Required(ErrorMessage = "Service street address is required")]
  [MaxLength(255, ErrorMessage = "Street address cannot exceed 255 characters")]
  public string ServiceStreet { get; set; } = string.Empty;

  [Required(ErrorMessage = "Service city is required")]
  [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters")]
  public string ServiceCity { get; set; } = string.Empty;

  [Required(ErrorMessage = "Service state is required")]
  [MaxLength(100, ErrorMessage = "State cannot exceed 100 characters")]
  public string ServiceState { get; set; } = string.Empty;

  [Required(ErrorMessage = "Service postal code is required")]
  [MaxLength(20, ErrorMessage = "Postal code cannot exceed 20 characters")]
  public string ServicePostalCode { get; set; } = string.Empty;

  [Required(ErrorMessage = "Service country is required")]
  [MaxLength(100, ErrorMessage = "Country cannot exceed 100 characters")]
  public string ServiceCountry { get; set; } = string.Empty;

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

  public Currency Currency { get; set; } = Currency.TRY;

  // Additional information
  [MaxLength(2000, ErrorMessage = "Notes cannot exceed 2000 characters")]
  public string? Notes { get; set; }

  [MaxLength(2000, ErrorMessage = "Internal notes cannot exceed 2000 characters")]
  public string? InternalNotes { get; set; }

  public bool RequiresCustomerSignature { get; set; } = false;

  // Assignment
  public List<Guid>? TechnicianIds { get; set; }
  public Guid? PrimaryTechnicianId { get; set; }

  // Recurrence
  public bool IsRecurring { get; set; } = false;
  public RecurrencePatternDto? RecurrencePattern { get; set; }

  // Options
  public bool SendNotificationToCustomer { get; set; } = true;
  public bool SendNotificationToTechnicians { get; set; } = true;
  public bool AutoCreateQuote { get; set; } = false;
}
