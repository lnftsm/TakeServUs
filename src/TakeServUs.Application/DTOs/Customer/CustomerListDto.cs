using TakeServUs.Application.Common.Mappings;
using AutoMapper;

namespace TakeServUs.Application.DTOs.Customer;

public class CustomerListDto : IMapFrom<Domain.Entities.Customer>
{
  public Guid Id { get; set; }
  public Guid UserId { get; set; }
  public string FullName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;
  public string? CompanyName { get; set; }
  public string City { get; set; } = string.Empty;
  public string Country { get; set; } = string.Empty;
  public bool IsVip { get; set; }
  public decimal CurrentBalance { get; set; }
  public int TotalJobs { get; set; }
  public int CompletedJobs { get; set; }
  public decimal TotalSpent { get; set; }
  public double? AverageRating { get; set; }
  public bool IsActive { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? LastJobDate { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Domain.Entities.Customer, CustomerListDto>()
        .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.User.FullName.GetFullName()))
        .ForMember(d => d.Email, opt => opt.MapFrom(s => s.User.Email.Value))
        .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.User.PhoneNumber.ToString()))
        .ForMember(d => d.City, opt => opt.MapFrom(s => s.Address.City))
        .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Address.Country))
        .ForMember(d => d.TotalJobs, opt => opt.MapFrom(s => s.Jobs.Count))
        .ForMember(d => d.CompletedJobs, opt => opt.MapFrom(s => s.Jobs.Count(j => j.CurrentStatus == Domain.Enums.JobStatusType.Completed || j.CurrentStatus == Domain.Enums.JobStatusType.Paid)))
        .ForMember(d => d.TotalSpent, opt => opt.MapFrom(s => s.Invoices.Where(i => i.PaymentStatus == Domain.Enums.PaymentStatus.Paid).Sum(i => i.TotalAmount.Amount)))
        .ForMember(d => d.AverageRating, opt => opt.MapFrom(s => s.Feedbacks.Any() ? s.Feedbacks.Average(f => (double)f.OverallRating) : (double?)null))
        .ForMember(d => d.LastJobDate, opt => opt.MapFrom(s => s.Jobs.OrderByDescending(j => j.CreatedAt).FirstOrDefault() != null ? s.Jobs.OrderByDescending(j => j.CreatedAt).First().CreatedAt : (DateTime?)null))
        .ForMember(d => d.IsActive, opt => opt.MapFrom(s => !s.IsDeleted));
  }
}
