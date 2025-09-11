using System.ComponentModel.DataAnnotations;

namespace TakeServUs.Application.DTOs.Auth;

public class LoginRequestDto
{
  [Required(ErrorMessage = "Email is required")]
  [EmailAddress(ErrorMessage = "Invalid email format")]
  public string Email { get; set; } = string.Empty;

  [Required(ErrorMessage = "Password is required")]
  [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
  public string Password { get; set; } = string.Empty;

  public bool RememberMe { get; set; } = false;

  public string? DeviceId { get; set; }

  public string? DeviceName { get; set; }

  public string? IpAddress { get; set; }

  public string? UserAgent { get; set; }
}
