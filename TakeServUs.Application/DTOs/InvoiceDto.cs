using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.DTOs;

public class InvoiceDto
{
  public Guid Id { get; set; }
  public required string InvoiceNumber { get; set; }
  public Guid JobId { get; set; }
  public Guid CustomerId { get; set; }
  public required string CustomerName { get; set; }
  public DateTime InvoiceDate { get; set; }
  public DateTime DueDate { get; set; }
  public decimal TotalAmount { get; set; }
  public required string Currency { get; set; }
  public required PaymentStatus PaymentStatus { get; set; }
  public ICollection<InvoiceItemDto> Items { get; set; } = new List<InvoiceItemDto>();
}
