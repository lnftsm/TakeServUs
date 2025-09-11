using System.ComponentModel.DataAnnotations;

namespace TakeServUs.Application.DTOs.Auth;

public class ChangePasswordDto
{
  [Required(ErrorMessage = "Current password is required")]
  public string CurrentPassword { get; set; } = string.Empty;

  [Required(ErrorMessage = "New password is required")]
  [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
  [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
      ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character")]
  public string NewPassword { get; set; } = string.Empty;

  [Required(ErrorMessage = "Password confirmation is required")]
  [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
  public string ConfirmNewPassword { get; set; } = string.Empty;

  public bool LogoutAllDevices { get; set; } = false;
}
