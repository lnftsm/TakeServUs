using System.ComponentModel.DataAnnotations;

namespace TakeServUs.Application.DTOs.Auth;

public class RefreshTokenRequestDto
{
  [Required(ErrorMessage = "Access token is required")]
  public string AccessToken { get; set; } = string.Empty;

  [Required(ErrorMessage = "Refresh token is required")]
  public string RefreshToken { get; set; } = string.Empty;

  public string? DeviceId { get; set; }
}
