using TakeServUs.Domain.Common;
using TakeServUs.Domain.Enums;

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
