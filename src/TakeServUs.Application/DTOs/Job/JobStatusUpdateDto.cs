using TakeServUs.Domain.Enums;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Application.DTOs.Job;

public class JobStatusUpdateDto
{
  public JobStatusType Status { get; set; }
  public string? Notes { get; set; }
  public decimal? Latitude { get; set; }
  public decimal? Longitude { get; set; }
  public decimal? LocationAccuracy { get; set; }
  public DateTime? StatusChangedAt { get; set; }
  public decimal? ActualCost { get; set; }
  public Currency? ActualCostCurrency { get; set; }
  public string? PauseReason { get; set; }
  public string? CancellationReason { get; set; }
  public DateTime? ActualStartTime { get; set; }
  public DateTime? ActualEndTime { get; set; }
  public Guid? TechnicianId { get; set; }
  public bool SendCustomerNotification { get; set; } = true;
  public bool RequiresApproval { get; set; } = false;
  public JobStatusType? CurrentStatus { get; set; }
  public bool IsValidTransition()
  {
    if (!CurrentStatus.HasValue)
      return true; // Skip validation if current status not provided

    // Define valid status transitions based on business workflow
    var validTransitions = CurrentStatus.Value switch
    {
      JobStatusType.Created => new[] { JobStatusType.Assigned, JobStatusType.Cancelled },
      JobStatusType.Assigned => new[] { JobStatusType.Accepted, JobStatusType.Cancelled },
      JobStatusType.Accepted => new[] { JobStatusType.EnRoute, JobStatusType.Cancelled },
      JobStatusType.EnRoute => new[] { JobStatusType.Started, JobStatusType.Cancelled },
      JobStatusType.Started => new[] { JobStatusType.Paused, JobStatusType.Completed, JobStatusType.Cancelled },
      JobStatusType.Paused => new[] { JobStatusType.Started, JobStatusType.Cancelled },
      JobStatusType.Completed => new[] { JobStatusType.Invoiced },
      JobStatusType.Invoiced => new[] { JobStatusType.Paid },
      JobStatusType.Cancelled => Array.Empty<JobStatusType>(), // No transitions from cancelled
      JobStatusType.Paid => Array.Empty<JobStatusType>(), // No transitions from paid
      _ => Array.Empty<JobStatusType>()
    };

    return validTransitions.Contains(Status);
  }

  public bool IsLocationRequired()
  {
    return Status is JobStatusType.EnRoute or JobStatusType.Started or JobStatusType.Completed;
  }

  public bool HasRequiredDataForStatus()
  {
    return Status switch
    {
      JobStatusType.Cancelled => !string.IsNullOrWhiteSpace(CancellationReason),
      JobStatusType.Paused => !string.IsNullOrWhiteSpace(PauseReason),
      JobStatusType.Completed => ActualEndTime.HasValue || StatusChangedAt.HasValue,
      JobStatusType.Started => ActualStartTime.HasValue || StatusChangedAt.HasValue,
      _ => true
    };
  }

  public string GetStatusChangeMessage()
  {
    return Status switch
    {
      JobStatusType.Created => "Job has been created",
      JobStatusType.Assigned => $"Job has been assigned{(TechnicianId.HasValue ? " to technician" : "")}",
      JobStatusType.Accepted => "Job has been accepted by technician",
      JobStatusType.EnRoute => "Technician is en route to job location",
      JobStatusType.Started => "Job work has started",
      JobStatusType.Paused => $"Job has been paused{(!string.IsNullOrWhiteSpace(PauseReason) ? $": {PauseReason}" : "")}",
      JobStatusType.Completed => "Job has been completed",
      JobStatusType.Cancelled => $"Job has been cancelled{(!string.IsNullOrWhiteSpace(CancellationReason) ? $": {CancellationReason}" : "")}",
      JobStatusType.Invoiced => "Job has been invoiced",
      JobStatusType.Paid => "Job payment has been received",
      _ => $"Job status changed to {Status}"
    };
  }

  public (bool IsValid, List<string> Errors) Validate()
  {
    var errors = new List<string>();

    // Check valid transition
    if (CurrentStatus.HasValue && !IsValidTransition())
    {
      errors.Add($"Invalid status transition from {CurrentStatus.Value} to {Status}");
    }

    // Check required data for specific statuses
    if (!HasRequiredDataForStatus())
    {
      errors.Add(Status switch
      {
        JobStatusType.Cancelled => "Cancellation reason is required",
        JobStatusType.Paused => "Pause reason is required",
        JobStatusType.Completed => "Actual end time is required for completion",
        JobStatusType.Started => "Actual start time is required for starting job",
        _ => "Required data is missing for status update"
      });
    }

    if (IsLocationRequired() && (!Latitude.HasValue || !Longitude.HasValue))
    {
      errors.Add($"GPS location is required for status: {Status}");
    }
    if (Latitude.HasValue && (Latitude.Value < -90 || Latitude.Value > 90))
    {
      errors.Add("Latitude must be between -90 and 90");
    }
    if (Longitude.HasValue && (Longitude.Value < -180 || Longitude.Value > 180))
    {
      errors.Add("Longitude must be between -180 and 180");
    }
    if (ActualCost.HasValue && ActualCost.Value < 0)
    {
      errors.Add("Actual cost cannot be negative");
    }
    if (ActualStartTime.HasValue && ActualEndTime.HasValue && ActualEndTime.Value < ActualStartTime.Value)
    {
      errors.Add("End time cannot be before start time");
    }

    return (errors.Count == 0, errors);
  }
}
