using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.DTOs;

public class NotificationDto
{
  public Guid Id { get; set; }
  public NotificationType Type { get; set; }
  public required string Title { get; set; }
  public required string Message { get; set; }
  public bool IsRead { get; set; }
  public DateTime CreatedAt { get; set; }
}
