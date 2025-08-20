using TakeServUs.Domain.Common;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Domain.Entities;

public class Quote : AuditableSoftDeletableEntity
{
  public required string QuoteNumber { get; set; }
  public Guid JobId { get; set; }
  public required DateTime QuoteDate { get; set; }
  public required DateTime ValidUntil { get; set; }
  public required Money SubTotal { get; set; }
  public Money? TaxAmount { get; set; }
  public Money? DiscountAmount { get; set; }
  public required Money TotalAmount { get; set; }
  public string? Terms { get; set; }
  public string? Notes { get; set; }
  public bool IsAccepted { get; set; } = false;
  public DateTime? AcceptedAt { get; set; }
  public string? AcceptedBy { get; set; }

  // Relations
  public virtual Job Job { get; set; } = default!;
  public virtual ICollection<QuoteItem> Items { get; set; } = default!;

  public Quote()
  {
    Items = new HashSet<QuoteItem>();
  }
}

