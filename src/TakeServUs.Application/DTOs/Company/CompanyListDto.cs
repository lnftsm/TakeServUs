using TakeServUs.Application.Common.Mappings;
using AutoMapper;

namespace TakeServUs.Application.DTOs.Company;

public class CompanyListDto : IMapFrom<Domain.Entities.Company>
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string? LogoUrl { get; set; }
  public string City { get; set; } = string.Empty;
  public string Country { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Phone { get; set; } = string.Empty;
  public string? SubscriptionPlan { get; set; }
  public DateTime? SubscriptionExpiry { get; set; }
  public int TotalUsers { get; set; }
  public int TotalTechnicians { get; set; }
  public int TotalCustomers { get; set; }
  public bool IsActive { get; set; }
  public DateTime CreatedAt { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Domain.Entities.Company, CompanyListDto>()
        .ForMember(d => d.City, opt => opt.MapFrom(s => s.Address.City))
        .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Address.Country))
        .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email.Value))
        .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.PhoneNumber.ToString()))
        .ForMember(d => d.TotalUsers, opt => opt.MapFrom(s => s.Users.Count))
        .ForMember(d => d.TotalTechnicians, opt => opt.MapFrom(s => s.Technicians.Count))
        .ForMember(d => d.TotalCustomers, opt => opt.MapFrom(s => s.Customers.Count))
        .ForMember(d => d.IsActive, opt => opt.MapFrom(s => !s.IsDeleted));
  }
}
