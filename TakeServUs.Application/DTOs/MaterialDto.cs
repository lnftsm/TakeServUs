namespace TakeServUs.Application.DTOs;

public class MaterialDto
{
  public Guid Id { get; set; }
  public required string Code { get; set; }
  public required string Name { get; set; }
  public required string Unit { get; set; }
  public required decimal UnitCost { get; set; }
  public required decimal QuantityInStock { get; set; }
}
