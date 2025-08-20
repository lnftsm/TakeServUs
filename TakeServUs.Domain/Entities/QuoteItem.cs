using TakeServUs.Domain.Common;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Domain.Entities
{
  public class QuoteItem : BaseEntity
  {
    public Guid QuoteId { get; set; }
    public required string Description { get; set; }
    public required decimal Quantity { get; set; }
    public required Money UnitPrice { get; set; }
    public required Money TotalPrice { get; set; }
    public string? Notes { get; set; }

    // Relations
    public virtual Quote Quote { get; set; } = default!;
  }
}
