namespace TakeServUs.Application.DTOs.Auth;

public class RegisterResponseDto
{
  public Guid UserId { get; set; }
  public string Email { get; set; } = string.Empty;
  public string FullName { get; set; } = string.Empty;
  public string Role { get; set; } = string.Empty;
  public bool EmailConfirmed { get; set; }
  public bool PhoneNumberConfirmed { get; set; }
  public string? AccessToken { get; set; }
  public string? RefreshToken { get; set; }
  public DateTime? TokenExpiry { get; set; }
  public Guid? CompanyId { get; set; }
  public string? CompanyName { get; set; }
  public bool RequiresEmailConfirmation { get; set; }
  public bool RequiresPhoneConfirmation { get; set; }
  public string Message { get; set; } = "Registration successful";
}
