using TakeServUs.Domain.Common;
using TakeServUs.Domain.Enums;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Domain.Entities;

public class JobStatus : AuditableEntity
{
  public Guid JobId { get; set; }
  public JobStatusType Status { get; set; }
  public DateTime ChangedAt { get; set; }
  public string? ChangedBy { get; set; }
  public string? Notes { get; set; }
  public GpsCoordinate? Location { get; set; }

  // Relations
  public virtual Job Job { get; set; } = default!;
}
