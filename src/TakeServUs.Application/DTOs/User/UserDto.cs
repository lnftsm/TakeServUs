using TakeServUs.Application.Common.Mappings;
using TakeServUs.Domain.Enums;
using AutoMapper;

namespace TakeServUs.Application.DTOs.User;

public class UserDto : IMapFrom<Domain.Entities.User>
{
  public Guid Id { get; set; }
  public string Email { get; set; } = string.Empty;
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string? MiddleName { get; set; }
  public string FullName { get; set; } = string.Empty;
  public string PhoneCountryCode { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;
  public UserRole Role { get; set; }
  public string RoleDisplayName { get; set; } = string.Empty;
  public string? ProfileImageUrl { get; set; }
  public string PreferredLanguage { get; set; } = "en";
  public string PreferredTheme { get; set; } = "system";
  public bool EmailConfirmed { get; set; }
  public bool PhoneNumberConfirmed { get; set; }
  public DateTime? LastLoginAt { get; set; }

  // Company information
  public Guid? CompanyId { get; set; }
  public string? CompanyName { get; set; }

  // Related entity IDs
  public Guid? TechnicianId { get; set; }
  public Guid? CustomerId { get; set; }

  // Statistics
  public int NotificationCount { get; set; }
  public int UnreadNotifications { get; set; }

  // Status
  public bool IsActive { get; set; }
  public bool IsBlocked { get; set; }
  public string? BlockReason { get; set; }

  // Audit fields
  public DateTime CreatedAt { get; set; }
  public DateTime? LastModifiedAt { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Domain.Entities.User, UserDto>()
        .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email.Value))
        .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FullName.FirstName))
        .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.FullName.LastName))
        .ForMember(d => d.MiddleName, opt => opt.MapFrom(s => s.FullName.MiddleName))
        .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FullName.GetFullName()))
        .ForMember(d => d.PhoneCountryCode, opt => opt.MapFrom(s => s.PhoneNumber.CountryCode))
        .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber.Number))
        .ForMember(d => d.RoleDisplayName, opt => opt.MapFrom(s => GetRoleDisplayName(s.Role)))
        .ForMember(d => d.CompanyName, opt => opt.MapFrom(s => s.Company != null ? s.Company.Name : null))
        .ForMember(d => d.NotificationCount, opt => opt.MapFrom(s => s.Notifications.Count))
        .ForMember(d => d.UnreadNotifications, opt => opt.MapFrom(s => s.Notifications.Count(n => !n.IsRead)))
        .ForMember(d => d.IsActive, opt => opt.MapFrom(s => !s.IsDeleted))
        .ForMember(d => d.IsBlocked, opt => opt.MapFrom(s => false)); // This would be determined by business logic
  }

  private static string GetRoleDisplayName(UserRole role)
  {
    return role switch
    {
      UserRole.SuperAdmin => "Super Administrator",
      UserRole.CompanyOwner => "Company Owner",
      UserRole.CompanyAdmin => "Company Administrator",
      UserRole.Technician => "Technician",
      UserRole.Customer => "Customer",
      _ => role.ToString()
    };
  }
}
