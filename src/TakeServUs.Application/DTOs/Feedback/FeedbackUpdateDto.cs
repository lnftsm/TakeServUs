using System.ComponentModel.DataAnnotations;
using TakeServUs.Domain.Enums;

namespace TakeServUs.Application.DTOs.Feedback;

public class FeedbackUpdateDto
{
  [Required(ErrorMessage = "Job ID is required")]
  public Guid JobId { get; set; }

  // Customer ID will be retrieved from the job or current user context
  public Guid? CustomerId { get; set; }

  [Required(ErrorMessage = "Overall rating is required")]
  [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
  public FeedbackRating OverallRating { get; set; }

  [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
  public FeedbackRating? QualityRating { get; set; }

  [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
  public FeedbackRating? PunctualityRating { get; set; }

  [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
  public FeedbackRating? ProfessionalismRating { get; set; }

  [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
  public FeedbackRating? ValueRating { get; set; }

  [MaxLength(2000, ErrorMessage = "Comments cannot exceed 2000 characters")]
  public string? Comments { get; set; }

  public bool IsPublic { get; set; } = true;

  // Optional: Allow anonymous feedback
  public bool IsAnonymous { get; set; } = false;
}
