namespace TakeServUs.Application.DTOs;

public class CustomerDto
{
  public Guid Id { get; set; }
  public required string FullName { get; set; } // From related User
  public required string Email { get; set; } // From related User
  public required string PhoneNumber { get; set; } // From related User
  public string? CompanyName { get; set; }
  public required string Address { get; set; }
  public bool IsVip { get; set; }
}
