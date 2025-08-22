using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.DTOs;

public class JobDto
{
  public Guid Id { get; set; }
  public required string JobNumber { get; set; }
  public required string Title { get; set; }
  public string? Description { get; set; }
  public required JobStatusType CurrentStatus { get; set; }
  public required JobPriority Priority { get; set; }
  public Guid CustomerId { get; set; }
  public required string CustomerName { get; set; }
  public required string ServiceAddress { get; set; }
  public DateTime? ScheduledStartTime { get; set; }
  public DateTime? ScheduledEndTime { get; set; }
}
