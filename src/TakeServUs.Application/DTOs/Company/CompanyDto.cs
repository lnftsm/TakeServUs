using TakeServUs.Application.Common.Mappings;
using AutoMapper;

namespace TakeServUs.Application.DTOs.Company;

public class CompanyDto : IMapFrom<Domain.Entities.Company>
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string? Description { get; set; }
  public string? LogoUrl { get; set; }

  // Address components
  public string Street { get; set; } = string.Empty;
  public string City { get; set; } = string.Empty;
  public string State { get; set; } = string.Empty;
  public string PostalCode { get; set; } = string.Empty;
  public string Country { get; set; } = string.Empty;
  public decimal? Latitude { get; set; }
  public decimal? Longitude { get; set; }

  // Contact information
  public string PhoneCountryCode { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string? Website { get; set; }

  // Business information
  public string? TaxNumber { get; set; }
  public string? RegistrationNumber { get; set; }
  public string Currency { get; set; } = "TRY";
  public string TimeZone { get; set; } = "UTC";
  public Dictionary<string, object>? WorkingHours { get; set; }

  // Subscription information
  public string? SubscriptionPlan { get; set; }
  public DateTime? SubscriptionExpiry { get; set; }
  public int MaxTechnicians { get; set; }
  public int MaxJobsPerMonth { get; set; }

  // Statistics
  public int TotalUsers { get; set; }
  public int TotalTechnicians { get; set; }
  public int TotalCustomers { get; set; }
  public int ActiveJobs { get; set; }

  // Audit fields
  public DateTime CreatedAt { get; set; }
  public DateTime? LastModifiedAt { get; set; }
  public bool IsActive { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Domain.Entities.Company, CompanyDto>()
        .ForMember(d => d.Street, opt => opt.MapFrom(s => s.Address.Street))
        .ForMember(d => d.City, opt => opt.MapFrom(s => s.Address.City))
        .ForMember(d => d.State, opt => opt.MapFrom(s => s.Address.State))
        .ForMember(d => d.PostalCode, opt => opt.MapFrom(s => s.Address.PostalCode))
        .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Address.Country))
        .ForMember(d => d.Latitude, opt => opt.MapFrom(s => s.Address.Coordinates != null ? s.Address.Coordinates.Latitude : (decimal?)null))
        .ForMember(d => d.Longitude, opt => opt.MapFrom(s => s.Address.Coordinates != null ? s.Address.Coordinates.Longitude : (decimal?)null))
        .ForMember(d => d.PhoneCountryCode, opt => opt.MapFrom(s => s.PhoneNumber.CountryCode))
        .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber.Number))
        .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email.Value))
        .ForMember(d => d.Currency, opt => opt.MapFrom(s => s.Currency.ToString()))
        .ForMember(d => d.TotalUsers, opt => opt.MapFrom(s => s.Users.Count))
        .ForMember(d => d.TotalTechnicians, opt => opt.MapFrom(s => s.Technicians.Count))
        .ForMember(d => d.TotalCustomers, opt => opt.MapFrom(s => s.Customers.Count))
        .ForMember(d => d.ActiveJobs, opt => opt.MapFrom(s => s.Jobs.Count(j => !j.IsDeleted && j.CurrentStatus != Domain.Enums.JobStatusType.Completed)))
        .ForMember(d => d.IsActive, opt => opt.MapFrom(s => !s.IsDeleted))
        .ForMember(d => d.WorkingHours, opt => opt.MapFrom(s => string.IsNullOrEmpty(s.WorkingHours) ? null : System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(s.WorkingHours, new System.Text.Json.JsonSerializerOptions())));
  }
}
