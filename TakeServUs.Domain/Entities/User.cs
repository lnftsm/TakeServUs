using TakeServUs.Domain.Common;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Domain.Entities;

public class User : AuditableSoftDeletableEntity
{
  public required Email Email { get; set; }
  public required string PasswordHash { get; set; }
  public required FullName FullName { get; set; }
  public required PhoneNumber PhoneNumber { get; set; }
  public required UserRole Role { get; set; }
  public string? ProfileImageUrl { get; set; }
  public string PreferredLanguage { get; set; } = "en";
  public string PreferredTheme { get; set; } = "system";
  public bool EmailConfirmed { get; set; } = false;
  public bool PhoneNumberConfirmed { get; set; } = false;
  public DateTime? LastLoginAt { get; set; }
  public string? RefreshToken { get; set; }
  public DateTime? RefreshTokenExpiryTime { get; set; }

  // Relations
  public Guid? CompanyId { get; set; }
  public virtual Company Company { get; set; } = default!;
  public virtual ICollection<Notification> Notifications { get; set; } = default!;

  public User()
  {
    Notifications = new HashSet<Notification>();
  }
}

public enum UserRole
{
  SuperAdmin = 1,
  CompanyOwner = 2,
  CompanyAdmin = 3,
  Technician = 4,
  Customer = 5
}
