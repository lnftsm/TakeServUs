namespace TakeServUs.Application.DTOs;

public class InvoiceItemDto
{
  public Guid Id { get; set; }
  public required string Description { get; set; }
  public required decimal Quantity { get; set; }
  public required decimal UnitPrice { get; set; }
  public required decimal TotalPrice { get; set; }
}
