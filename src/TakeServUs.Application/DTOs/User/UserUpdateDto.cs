using System.ComponentModel.DataAnnotations;
using TakeServUs.Domain.Enums;

namespace TakeServUs.Application.DTOs.User;

public class UserUpdateDto
{
  [EmailAddress(ErrorMessage = "Invalid email format")]
  public string? Email { get; set; }

  [MaxLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
  public string? FirstName { get; set; }

  [MaxLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
  public string? LastName { get; set; }

  [MaxLength(100, ErrorMessage = "Middle name cannot exceed 100 characters")]
  public string? MiddleName { get; set; }

  [RegularExpression(@"^\+\d{1,4}$", ErrorMessage = "Invalid country code format")]
  public string? PhoneCountryCode { get; set; }

  [RegularExpression(@"^\d{7,15}$", ErrorMessage = "Invalid phone number format")]
  public string? PhoneNumber { get; set; }

  public UserRole? Role { get; set; }

  [Url(ErrorMessage = "Invalid profile image URL")]
  public string? ProfileImageUrl { get; set; }

  public string? PreferredLanguage { get; set; }
  public string? PreferredTheme { get; set; }

  public bool? EmailConfirmed { get; set; }
  public bool? PhoneNumberConfirmed { get; set; }
}
