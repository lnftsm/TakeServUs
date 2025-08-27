using TakeServUs.Domain.Common;
using TakeServUs.Domain.Enums;

namespace TakeServUs.Domain.Entities;

public class Feedback : AuditableSoftDeletableEntity
{
  public Guid JobId { get; set; }
  public Guid CustomerId { get; set; }
  public FeedbackRating OverallRating { get; set; }
  public FeedbackRating? QualityRating { get; set; }
  public FeedbackRating? PunctualityRating { get; set; }
  public FeedbackRating? ProfessionalismRating { get; set; }
  public FeedbackRating? ValueRating { get; set; }
  public string? Comments { get; set; }
  public string? Response { get; set; } // Company response to feedback
  public DateTime? RespondedAt { get; set; }
  public string? RespondedBy { get; set; }
  public bool IsPublic { get; set; } = true;

  // Relations
  public virtual Job Job { get; set; } = default!;
  public virtual Customer Customer { get; set; } = default!;
}
