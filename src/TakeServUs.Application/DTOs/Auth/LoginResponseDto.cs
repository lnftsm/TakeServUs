namespace TakeServUs.Application.DTOs.Auth;

public class LoginResponseDto
{
  // User information
  public Guid UserId { get; set; }
  public string Email { get; set; } = string.Empty;
  public string FullName { get; set; } = string.Empty;
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Role { get; set; } = string.Empty;
  public string? ProfileImageUrl { get; set; }

  // Company information
  public Guid? CompanyId { get; set; }
  public string? CompanyName { get; set; }
  public string? CompanyLogoUrl { get; set; }
  public string? SubscriptionPlan { get; set; }
  public DateTime? SubscriptionExpiry { get; set; }

  // Role-specific IDs
  public Guid? TechnicianId { get; set; }
  public Guid? CustomerId { get; set; }

  // Tokens
  public string AccessToken { get; set; } = string.Empty;
  public string RefreshToken { get; set; } = string.Empty;
  public DateTime TokenExpiry { get; set; }
  public string TokenType { get; set; } = "Bearer";

  // User preferences
  public string PreferredLanguage { get; set; } = "en";
  public string PreferredTheme { get; set; } = "system";
  public string TimeZone { get; set; } = "UTC";
  public string Currency { get; set; } = "TRY";

  // Security flags
  public bool EmailConfirmed { get; set; }
  public bool PhoneNumberConfirmed { get; set; }
  public bool TwoFactorEnabled { get; set; }
  public bool RequirePasswordChange { get; set; }

  // Permissions (for frontend)
  public List<string> Permissions { get; set; } = new();

  // Navigation/Menu items based on role
  public List<MenuItemDto> MenuItems { get; set; } = new();

  // Initial dashboard data
  public DashboardSummaryDto? DashboardSummary { get; set; }

  // Session information
  public string SessionId { get; set; } = string.Empty;
  public DateTime LoginTime { get; set; }
  public string? IpAddress { get; set; }
  public string? UserAgent { get; set; }
  public string? DeviceId { get; set; }
}

public class MenuItemDto
{
  public string Title { get; set; } = string.Empty;
  public string Icon { get; set; } = string.Empty;
  public string Path { get; set; } = string.Empty;
  public int Order { get; set; }
  public List<MenuItemDto>? Children { get; set; }
  public bool RequiresSubscription { get; set; } = false;
  public string? Badge { get; set; } // For notification counts, etc.
}

public class DashboardSummaryDto
{
  // For Company/Admin
  public int? TotalJobs { get; set; }
  public int? ActiveJobs { get; set; }
  public int? PendingJobs { get; set; }
  public int? CompletedJobsToday { get; set; }

  // For Technician
  public int? MyActiveJobs { get; set; }
  public int? MyPendingJobs { get; set; }
  public int? MyCompletedJobsToday { get; set; }
  public string? NextJobLocation { get; set; }
  public DateTime? NextJobTime { get; set; }

  // For Customer
  public int? MyTotalJobs { get; set; }
  public int? MyOpenJobs { get; set; }
  public decimal? OutstandingBalance { get; set; }

  // Common
  public int UnreadNotifications { get; set; }
  public List<RecentActivityDto>? RecentActivities { get; set; }
}

public class RecentActivityDto
{
  public string Type { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public DateTime Timestamp { get; set; }
  public string? ActionUrl { get; set; }
  public string Icon { get; set; } = string.Empty;
}
