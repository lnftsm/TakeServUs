namespace TakeServUs.Application.DTOs.Material;

public class MaterialUsageDto
{
  public Guid Id { get; set; }
  public Guid MaterialId { get; set; }
  public string MaterialCode { get; set; } = string.Empty;
  public string MaterialName { get; set; } = string.Empty;
  public Guid JobId { get; set; }
  public string JobNumber { get; set; } = string.Empty;
  public string CustomerName { get; set; } = string.Empty;
  public decimal Quantity { get; set; }
  public string Unit { get; set; } = string.Empty;
  public decimal? UnitCost { get; set; }
  public decimal? TotalCost { get; set; }
  public DateTime UsedAt { get; set; }
  public string? UsedByTechnicianName { get; set; }
  public string? Notes { get; set; }
}
