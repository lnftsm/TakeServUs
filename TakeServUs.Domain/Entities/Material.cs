using TakeServUs.Domain.Common;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Domain.Entities;

public class Material : AuditableSoftDeletableEntity
{
  public Guid CompanyId { get; set; }
  public required string Code { get; set; }
  public required string Name { get; set; }
  public string? Description { get; set; }
  public required string Unit { get; set; } // piece, meter, kg, etc.
  public required Money UnitCost { get; set; }
  public Money? UnitPrice { get; set; }
  public decimal QuantityInStock { get; set; }
  public decimal? MinimumStock { get; set; }
  public string? Category { get; set; }
  public string? Supplier { get; set; }
  public string? ImageUrl { get; set; }

  // Relations
  public virtual Company Company { get; set; } = default!;
  public virtual ICollection<JobMaterial> JobMaterials { get; set; } = default!;

  public Material()
  {
    JobMaterials = new HashSet<JobMaterial>();
  }
}

