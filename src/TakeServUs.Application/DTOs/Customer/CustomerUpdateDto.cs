using System.ComponentModel.DataAnnotations;

namespace TakeServUs.Application.DTOs.Customer;

public class CustomerUpdateDto
{
  [MaxLength(200, ErrorMessage = "Company name cannot exceed 200 characters")]
  public string? CompanyName { get; set; }

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

  // Customer specific fields
  [MaxLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
  public string? Notes { get; set; }

  public bool? IsVip { get; set; }

  [Range(0, double.MaxValue, ErrorMessage = "Credit limit must be a positive value")]
  public decimal? CreditLimit { get; set; }

  // User information updates (optional)
  public CustomerUserUpdateDto? UserUpdate { get; set; }
}

public class CustomerUserUpdateDto
{
  [MaxLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
  public string? FirstName { get; set; }

  [MaxLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
  public string? LastName { get; set; }

  public string? MiddleName { get; set; }

  [EmailAddress(ErrorMessage = "Invalid email format")]
  public string? Email { get; set; }

  [RegularExpression(@"^\+\d{1,4}$", ErrorMessage = "Invalid country code format")]
  public string? PhoneCountryCode { get; set; }

  [RegularExpression(@"^\d{7,15}$", ErrorMessage = "Invalid phone number format")]
  public string? PhoneNumber { get; set; }
}
