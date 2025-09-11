using System.ComponentModel.DataAnnotations;
using TakeServUs.Domain.Enums;

namespace TakeServUs.Application.DTOs.Auth;

public class RegisterRequestDto
{
  [Required(ErrorMessage = "Email is required")]
  [EmailAddress(ErrorMessage = "Invalid email format")]
  public string Email { get; set; } = string.Empty;

  [Required(ErrorMessage = "Password is required")]
  [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
  [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
      ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character")]
  public string Password { get; set; } = string.Empty;

  [Required(ErrorMessage = "Password confirmation is required")]
  [Compare("Password", ErrorMessage = "Passwords do not match")]
  public string ConfirmPassword { get; set; } = string.Empty;

  [Required(ErrorMessage = "First name is required")]
  [MaxLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
  public string FirstName { get; set; } = string.Empty;

  [Required(ErrorMessage = "Last name is required")]
  [MaxLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
  public string LastName { get; set; } = string.Empty;

  public string? MiddleName { get; set; }

  [Required(ErrorMessage = "Phone country code is required")]
  [RegularExpression(@"^\+\d{1,4}$", ErrorMessage = "Invalid country code format")]
  public string PhoneCountryCode { get; set; } = string.Empty;

  [Required(ErrorMessage = "Phone number is required")]
  [RegularExpression(@"^\d{7,15}$", ErrorMessage = "Invalid phone number format")]
  public string PhoneNumber { get; set; } = string.Empty;

  public UserRole Role { get; set; } = UserRole.Customer;

  public Guid? CompanyId { get; set; }

  // For company owner registration
  public string? CompanyName { get; set; }

  // Terms acceptance
  [Required(ErrorMessage = "You must accept the terms and conditions")]
  public bool AcceptTerms { get; set; }

  public bool AcceptMarketing { get; set; } = false;

  public string PreferredLanguage { get; set; } = "en";

  public string PreferredTheme { get; set; } = "system";
}
