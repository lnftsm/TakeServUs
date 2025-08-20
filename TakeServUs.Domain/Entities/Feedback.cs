using TakeServUs.Domain.Common;

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

public enum FeedbackRating
{
  VeryPoor = 1,
  Poor = 2,
  Average = 3,
  Good = 4,
  Excellent = 5
}
