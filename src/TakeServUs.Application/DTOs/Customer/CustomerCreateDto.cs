using System.ComponentModel.DataAnnotations;

namespace TakeServUs.Application.DTOs.Customer;

public class CustomerCreateDto
{
  // Option 1: Create customer from existing user
  public Guid? UserId { get; set; }

  // Option 2: Create new user with customer
  public CustomerUserCreateDto? NewUser { get; set; }

  [Required(ErrorMessage = "Company ID is required")]
  public Guid CompanyId { get; set; }

  [MaxLength(200, ErrorMessage = "Company name cannot exceed 200 characters")]
  public string? CompanyName { get; set; }

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

  // Customer specific fields
  [MaxLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
  public string? Notes { get; set; }

  public bool IsVip { get; set; } = false;

  [Range(0, double.MaxValue, ErrorMessage = "Credit limit must be a positive value")]
  public decimal? CreditLimit { get; set; }
}

public class CustomerUserCreateDto
{
  [Required(ErrorMessage = "Email is required")]
  [EmailAddress(ErrorMessage = "Invalid email format")]
  public string Email { get; set; } = string.Empty;

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

  // Optional password - if not provided, system will generate one
  public string? Password { get; set; }

  public bool SendWelcomeEmail { get; set; } = true;
}
