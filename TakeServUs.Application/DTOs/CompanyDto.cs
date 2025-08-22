namespace TakeServUs.Application.DTOs;

public class CompanyDto
{
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public required string Email { get; set; }
  public required string PhoneNumber { get; set; }
  public required string Address { get; set; }
  public string? Website { get; set; }
}
