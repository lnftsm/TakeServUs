namespace TakeServUs.Application.DTOs.Auth;

public class LoginResponse
{
  public Guid UserId { get; set; }
  public Guid? CompanyId { get; set; }
  public string FullName { get; set; } = string.Empty;
  public string Role { get; set; } = string.Empty;
  public string Token { get; set; } = string.Empty;
}
