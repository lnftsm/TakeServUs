using System.ComponentModel.DataAnnotations;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Application.DTOs.Company;

public class CompanyCreateDto
{
  [Required(ErrorMessage = "Company name is required")]
  [MaxLength(200, ErrorMessage = "Company name cannot exceed 200 characters")]
  public string Name { get; set; } = string.Empty;

  [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
  public string? Description { get; set; }

  public string? LogoUrl { get; set; }

  // Address
  [Required(ErrorMessage = "Street address is required")]
  [MaxLength(255, ErrorMessage = "Street address cannot exceed 255 characters")]
  public string Street { get; set; } = string.Empty;

  [Required(ErrorMessage = "City is required")]
  [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters")]
  public string City { get; set; } = string.Empty;

  [Required(ErrorMessage = "State is required")]
  [MaxLength(100, ErrorMessage = "State cannot exceed 100 characters")]
  public string State { get; set; } = string.Empty;

  [Required(ErrorMessage = "Postal code is required")]
  [MaxLength(20, ErrorMessage = "Postal code cannot exceed 20 characters")]
  public string PostalCode { get; set; } = string.Empty;

  [Required(ErrorMessage = "Country is required")]
  [MaxLength(100, ErrorMessage = "Country cannot exceed 100 characters")]
  public string Country { get; set; } = string.Empty;

  public decimal? Latitude { get; set; }
  public decimal? Longitude { get; set; }

  // Contact
  [Required(ErrorMessage = "Phone country code is required")]
  [RegularExpression(@"^\+\d{1,4}$", ErrorMessage = "Invalid country code format")]
  public string PhoneCountryCode { get; set; } = string.Empty;

  [Required(ErrorMessage = "Phone number is required")]
  [RegularExpression(@"^\d{7,15}$", ErrorMessage = "Invalid phone number format")]
  public string PhoneNumber { get; set; } = string.Empty;

  [Required(ErrorMessage = "Email is required")]
  [EmailAddress(ErrorMessage = "Invalid email format")]
  public string Email { get; set; } = string.Empty;

  [Url(ErrorMessage = "Invalid website URL")]
  public string? Website { get; set; }

  // Business information
  public string? TaxNumber { get; set; }
  public string? RegistrationNumber { get; set; }

  public Currency Currency { get; set; } = Currency.TRY;

  [Required(ErrorMessage = "Time zone is required")]
  public string TimeZone { get; set; } = "UTC";

  public Dictionary<string, object>? WorkingHours { get; set; }

  // Initial subscription settings
  public string SubscriptionPlan { get; set; } = "Trial";
  public int MaxTechnicians { get; set; } = 10;
  public int MaxJobsPerMonth { get; set; } = 100;
}
