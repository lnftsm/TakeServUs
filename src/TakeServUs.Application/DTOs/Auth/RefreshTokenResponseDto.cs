namespace TakeServUs.Application.DTOs.Auth;

public class RefreshTokenResponseDto
{
  public string AccessToken { get; set; } = string.Empty;
  public string RefreshToken { get; set; } = string.Empty;
  public DateTime TokenExpiry { get; set; }
  public bool Success { get; set; }
  public string? Message { get; set; }
}
