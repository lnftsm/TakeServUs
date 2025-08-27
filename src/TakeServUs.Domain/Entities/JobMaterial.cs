using TakeServUs.Domain.Common;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Domain.Entities;

public class JobMaterial : AuditableEntity
{
  public Guid JobId { get; set; }
  public Guid MaterialId { get; set; }
  public decimal Quantity { get; set; }
  public Money? UnitCost { get; set; }
  public Money? UnitPrice { get; set; }
  public Money? TotalCost { get; set; }
  public Money? TotalPrice { get; set; }
  public DateTime UsedAt { get; set; }
  public string? Notes { get; set; }
  public Guid? UsedByTechnicianId { get; set; }

  // Relations
  public virtual Job Job { get; set; } = default!;
  public virtual Material Material { get; set; } = default!;
  public virtual Technician? UsedByTechnician { get; set; } = default!;
}
