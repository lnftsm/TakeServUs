using TakeServUs.Domain.Common;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Domain.Entities;

public class Job : AuditableSoftDeletableEntity
{
  public required string JobNumber { get; set; }
  public Guid CompanyId { get; set; }
  public Guid CustomerId { get; set; }
  public required string Title { get; set; }
  public string? Description { get; set; }
  public JobPriority Priority { get; set; } = JobPriority.Normal;
  public JobStatusType CurrentStatus { get; set; } = JobStatusType.Created;

  // Location
  public required Address ServiceAddress { get; set; }

  // Scheduling
  public DateTime? ScheduledStartTime { get; set; }
  public DateTime? ScheduledEndTime { get; set; }
  public DateTime? ActualStartTime { get; set; }
  public DateTime? ActualEndTime { get; set; }
  public int? EstimatedDurationMinutes { get; set; }

  // Pricing
  public Money? EstimatedCost { get; set; }
  public Money? ActualCost { get; set; }

  // Additional Info
  public string? Notes { get; set; }
  public string? InternalNotes { get; set; }
  public string? PhotoUrls { get; set; } // JSON array of photo URLs
  public string? Attachments { get; set; } // JSON array of attachment URLs
  public string? SignatureUrl { get; set; }
  public bool RequiresCustomerSignature { get; set; }
  public bool IsRecurring { get; set; }
  public string? RecurrencePattern { get; set; } // JSON for recurrence rules

  // Relations
  public virtual Company Company { get; set; } = default!;
  public virtual Customer Customer { get; set; } = default!;
  public virtual ICollection<JobAssignment> JobAssignments { get; set; } = default!;
  public virtual ICollection<JobStatus> StatusHistory { get; set; } = default!;
  public virtual ICollection<JobMaterial> JobMaterials { get; set; } = default!;
  public virtual Quote Quote { get; set; } = default!;
  public virtual Invoice Invoice { get; set; } = default!;
  public virtual Feedback Feedback { get; set; } = default!;

  public Job()
  {
    JobAssignments = new HashSet<JobAssignment>();
    StatusHistory = new HashSet<JobStatus>();
    JobMaterials = new HashSet<JobMaterial>();
  }
}

public enum JobPriority
{
  Low = 1,
  Normal = 2,
  High = 3,
  Urgent = 4,
  Emergency = 5
}

