using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.DTOs;

public class UserDto
{
  public Guid Id { get; set; }
  public required string FullName { get; set; }
  public required string Email { get; set; }
  public required string PhoneNumber { get; set; }
  public required UserRole Role { get; set; }
  public Guid? CompanyId { get; set; }
}
