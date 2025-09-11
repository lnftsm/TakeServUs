using System.ComponentModel.DataAnnotations;
using TakeServUs.Domain.Enums;

namespace TakeServUs.Application.DTOs.Technician;

public class TechnicianCreateDto
{
  // Option 1: Create technician from existing user
  public Guid? UserId { get; set; }

  // Option 2: Create new user with technician
  public TechnicianUserCreateDto? NewUser { get; set; }

  // Optional - will use current user's company if not provided
  public Guid? CompanyId { get; set; }

  [MaxLength(50, ErrorMessage = "Employee code cannot exceed 50 characters")]
  public string? EmployeeCode { get; set; }

  [MaxLength(200, ErrorMessage = "Specialization cannot exceed 200 characters")]
  public string? Specialization { get; set; }

  public List<SkillCreateDto>? Skills { get; set; }

  public bool IsAvailable { get; set; } = true;

  // Working schedule
  public List<DailyScheduleCreateDto>? WorkingSchedule { get; set; }

  // Vehicle information
  [MaxLength(50, ErrorMessage = "Vehicle type cannot exceed 50 characters")]
  public string? VehicleType { get; set; }

  [MaxLength(20, ErrorMessage = "Vehicle plate number cannot exceed 20 characters")]
  public string? VehiclePlateNumber { get; set; }
}

public class TechnicianUserCreateDto
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

public class SkillCreateDto
{
  [Required(ErrorMessage = "Skill name is required")]
  [MaxLength(100, ErrorMessage = "Skill name cannot exceed 100 characters")]
  public string Name { get; set; } = string.Empty;

  public SkillLevel Level { get; set; } = SkillLevel.Intermediate;
}

public class DailyScheduleCreateDto
{
  public DayOfWeek Day { get; set; }

  [Required(ErrorMessage = "Start time is required")]
  public TimeOnly StartTime { get; set; }

  [Required(ErrorMessage = "End time is required")]
  public TimeOnly EndTime { get; set; }
}
