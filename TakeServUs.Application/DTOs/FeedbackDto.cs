using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.DTOs;

public class FeedbackDto
{
  public Guid Id { get; set; }
  public Guid JobId { get; set; }
  public Guid CustomerId { get; set; }
  public required string CustomerName { get; set; }
  public FeedbackRating OverallRating { get; set; }
  public string? Comments { get; set; }
  public string? Response { get; set; }
  public DateTime CreatedAt { get; set; }
}
