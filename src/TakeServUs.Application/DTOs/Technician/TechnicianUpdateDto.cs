using System.ComponentModel.DataAnnotations;

namespace TakeServUs.Application.DTOs.Technician;

public class TechnicianUpdateDto
{
  [MaxLength(50, ErrorMessage = "Employee code cannot exceed 50 characters")]
  public string? EmployeeCode { get; set; }

  [MaxLength(200, ErrorMessage = "Specialization cannot exceed 200 characters")]
  public string? Specialization { get; set; }

  public List<SkillCreateDto>? Skills { get; set; }

  public bool? IsAvailable { get; set; }

  // Working schedule (replace all)
  public List<DailyScheduleCreateDto>? WorkingSchedule { get; set; }

  // Vehicle information
  [MaxLength(50, ErrorMessage = "Vehicle type cannot exceed 50 characters")]
  public string? VehicleType { get; set; }

  [MaxLength(20, ErrorMessage = "Vehicle plate number cannot exceed 20 characters")]
  public string? VehiclePlateNumber { get; set; }

  // User information updates (optional)
  public TechnicianUserUpdateDto? UserUpdate { get; set; }
}

public class TechnicianUserUpdateDto
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
