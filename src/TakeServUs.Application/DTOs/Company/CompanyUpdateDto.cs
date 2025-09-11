using System.ComponentModel.DataAnnotations;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Application.DTOs.Company;

public class CompanyUpdateDto
{
  [MaxLength(200, ErrorMessage = "Company name cannot exceed 200 characters")]
  public string? Name { get; set; }

  [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
  public string? Description { get; set; }

  public string? LogoUrl { get; set; }

  // Address (all fields optional for partial updates)
  [MaxLength(255, ErrorMessage = "Street address cannot exceed 255 characters")]
  public string? Street { get; set; }

  [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters")]
  public string? City { get; set; }

  [MaxLength(100, ErrorMessage = "State cannot exceed 100 characters")]
  public string? State { get; set; }

  [MaxLength(20, ErrorMessage = "Postal code cannot exceed 20 characters")]
  public string? PostalCode { get; set; }

  [MaxLength(100, ErrorMessage = "Country cannot exceed 100 characters")]
  public string? Country { get; set; }

  public decimal? Latitude { get; set; }
  public decimal? Longitude { get; set; }

  // Contact
  [RegularExpression(@"^\+\d{1,4}$", ErrorMessage = "Invalid country code format")]
  public string? PhoneCountryCode { get; set; }

  [RegularExpression(@"^\d{7,15}$", ErrorMessage = "Invalid phone number format")]
  public string? PhoneNumber { get; set; }

  [EmailAddress(ErrorMessage = "Invalid email format")]
  public string? Email { get; set; }

  [Url(ErrorMessage = "Invalid website URL")]
  public string? Website { get; set; }

  // Business information
  public string? TaxNumber { get; set; }
  public string? RegistrationNumber { get; set; }
  public Currency? Currency { get; set; }
  public string? TimeZone { get; set; }
  public Dictionary<string, object>? WorkingHours { get; set; }
}
