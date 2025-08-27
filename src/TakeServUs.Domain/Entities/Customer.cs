using TakeServUs.Domain.Common;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Domain.Entities;

public class Customer : AuditableSoftDeletableEntity
{
  public Guid UserId { get; set; }
  public Guid CompanyId { get; set; }
  public string? CompanyName { get; set; }
  public required Address Address { get; set; }
  public string? Notes { get; set; }
  public bool IsVip { get; set; }
  public decimal? CreditLimit { get; set; }
  public decimal CurrentBalance { get; set; } = 0;

  // Relations
  public virtual User User { get; set; } = default!;
  public virtual Company Company { get; set; } = default!;
  public virtual ICollection<Job> Jobs { get; set; } = default!;
  public virtual ICollection<Invoice> Invoices { get; set; } = default!;
  public virtual ICollection<Feedback> Feedbacks { get; set; } = default!;

  public Customer()
  {
    Jobs = new HashSet<Job>();
    Invoices = new HashSet<Invoice>();
    Feedbacks = new HashSet<Feedback>();
  }
}
