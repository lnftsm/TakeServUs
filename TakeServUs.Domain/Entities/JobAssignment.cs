using TakeServUs.Domain.Common;

namespace TakeServUs.Domain.Entities;

public class JobAssignment : AuditableEntity
{
  public Guid JobId { get; set; }
  public Guid TechnicianId { get; set; }
  public DateTime AssignedAt { get; set; }
  public DateTime? AcceptedAt { get; set; }
  public DateTime? DeclinedAt { get; set; }
  public DateTime? CompletedAt { get; set; }
  public bool IsPrimary { get; set; } = true;
  public string? Notes { get; set; }

  // Relations
  public virtual Job Job { get; set; } = default!;
  public virtual Technician Technician { get; set; } = default!;
}
