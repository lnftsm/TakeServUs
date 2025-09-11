using System.ComponentModel.DataAnnotations;

namespace TakeServUs.Application.DTOs.Auth;

public class ForgotPasswordDto
{
  [Required(ErrorMessage = "Email is required")]
  [EmailAddress(ErrorMessage = "Invalid email format")]
  public string Email { get; set; } = string.Empty;

  // For additional security
  public string? RecaptchaToken { get; set; }

  // Client information for logging
  public string? IpAddress { get; set; }
  public string? UserAgent { get; set; }
}
