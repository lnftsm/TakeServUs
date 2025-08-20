using TakeServUs.Domain.Common;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Domain.Entities;

public class Invoice : AuditableSoftDeletableEntity
{
  public required string InvoiceNumber { get; set; }
  public Guid JobId { get; set; }
  public Guid CustomerId { get; set; }
  public required DateTime InvoiceDate { get; set; }
  public required DateTime DueDate { get; set; }
  public required Money SubTotal { get; set; }
  public Money? TaxAmount { get; set; }
  public Money? DiscountAmount { get; set; }
  public required Money TotalAmount { get; set; }
  public required Money PaidAmount { get; set; }
  public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
  public PaymentMethod? PaymentMethod { get; set; }
  public DateTime? PaidAt { get; set; }
  public string? PaymentReference { get; set; }
  public string? Terms { get; set; }
  public string? Notes { get; set; }

  // Relations
  public virtual Job Job { get; set; } = default!;
  public virtual Customer Customer { get; set; } = default!;
  public virtual ICollection<InvoiceItem> Items { get; set; } = default!;

  public Invoice()
  {
    Items = new HashSet<InvoiceItem>();
  }
}

public enum PaymentMethod
{
  Cash = 1,
  Card = 2,
  Transfer = 3
}

public enum PaymentStatus
{
  Pending = 1,
  PartiallyPaid = 2,
  Paid = 3,
  Overdue = 4,
  Cancelled = 5,
  Refunded = 6
}

