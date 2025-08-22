namespace TakeServUs.Application.DTOs;

public class QuoteDto
{
  public Guid Id { get; set; }
  public required string QuoteNumber { get; set; }
  public Guid JobId { get; set; }
  public DateTime QuoteDate { get; set; }
  public DateTime ValidUntil { get; set; }
  public decimal TotalAmount { get; set; }
  public required string Currency { get; set; }
  public bool IsAccepted { get; set; }
  public DateTime? AcceptedAt { get; set; }
  public ICollection<QuoteItemDto> Items { get; set; } = new List<QuoteItemDto>();
}
