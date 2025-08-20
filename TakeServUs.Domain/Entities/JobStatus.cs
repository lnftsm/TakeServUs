using TakeServUs.Domain.Common;

namespace TakeServUs.Domain.Entities;

public class JobStatus : AuditableEntity
{
  public Guid JobId { get; set; }
  public JobStatusType Status { get; set; }
  public DateTime ChangedAt { get; set; }
  public string? ChangedBy { get; set; }
  public string? Notes { get; set; }
  public double? Latitude { get; set; }
  public double? Longitude { get; set; }

  // Relations
  public virtual Job Job { get; set; } = default!;
}

public enum JobStatusType
{
  Created = 1,
  Assigned = 2,
  Accepted = 3,
  EnRoute = 4,
  Started = 5,
  Paused = 6,
  Completed = 7,
  Cancelled = 8,
  Invoiced = 9,
  Paid = 10
}
