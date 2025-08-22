namespace TakeServUs.Application.DTOs;

public class TechnicianDto
{
  public Guid Id { get; set; }
  public required string FullName { get; set; } // From related User
  public required string Email { get; set; } // From related User
  public string? Specialization { get; set; }
  public bool IsAvailable { get; set; }
  public double? Rating { get; set; }
}
