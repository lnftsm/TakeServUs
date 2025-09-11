using System.ComponentModel.DataAnnotations;

namespace TakeServUs.Application.DTOs.User;

public class UserProfileDto
{
  public Guid Id { get; set; }
  public string Email { get; set; } = string.Empty;
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string? MiddleName { get; set; }
  public string FullName { get; set; } = string.Empty;
  public string PhoneCountryCode { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;
  public string? ProfileImageUrl { get; set; }
  public string PreferredLanguage { get; set; } = "en";
  public string PreferredTheme { get; set; } = "system";
  public bool EmailConfirmed { get; set; }
  public bool PhoneNumberConfirmed { get; set; }

  // Company information
  public CompanyProfileDto? Company { get; set; }

  // Role-specific information
  public TechnicianProfileDto? TechnicianProfile { get; set; }
  public CustomerProfileDto? CustomerProfile { get; set; }

  // Settings
  public UserSettingsDto Settings { get; set; } = new();

  // Statistics
  public UserStatisticsDto Statistics { get; set; } = new();

  public DateTime CreatedAt { get; set; }
  public DateTime? LastLoginAt { get; set; }
}

public class CompanyProfileDto
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string? LogoUrl { get; set; }
  public string Email { get; set; } = string.Empty;
  public string Phone { get; set; } = string.Empty;
  public string? SubscriptionPlan { get; set; }
  public DateTime? SubscriptionExpiry { get; set; }
}

public class TechnicianProfileDto
{
  public Guid Id { get; set; }
  public string? EmployeeCode { get; set; }
  public string? Specialization { get; set; }
  public double? Rating { get; set; }
  public int TotalJobsCompleted { get; set; }
  public bool IsAvailable { get; set; }
  public List<string> Skills { get; set; } = new();
}

public class CustomerProfileDto
{
  public Guid Id { get; set; }
  public string? CompanyName { get; set; }
  public bool IsVip { get; set; }
  public int TotalJobs { get; set; }
  public decimal TotalSpent { get; set; }
  public double? AverageRating { get; set; }
}

public class UserSettingsDto
{
  public bool EmailNotifications { get; set; } = true;
  public bool SmsNotifications { get; set; } = false;
  public bool PushNotifications { get; set; } = true;
  public bool TwoFactorEnabled { get; set; } = false;
  public string DateFormat { get; set; } = "MM/dd/yyyy";
  public string TimeFormat { get; set; } = "12h";
  public string Currency { get; set; } = "TRY";
  public string TimeZone { get; set; } = "UTC";
}

public class UserStatisticsDto
{
  public int TotalNotifications { get; set; }
  public int UnreadNotifications { get; set; }
  public DateTime? LastActivityDate { get; set; }
  public int LoginCount { get; set; }
  public Dictionary<string, int>? ActivitySummary { get; set; }
}
