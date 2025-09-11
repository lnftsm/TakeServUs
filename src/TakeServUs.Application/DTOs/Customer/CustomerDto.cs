using TakeServUs.Application.Common.Mappings;
using AutoMapper;

namespace TakeServUs.Application.DTOs.Customer;

public class CustomerDto : IMapFrom<Domain.Entities.Customer>
{
  public Guid Id { get; set; }
  public Guid UserId { get; set; }
  public Guid CompanyId { get; set; }

  // User information (denormalized for convenience)
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string FullName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;

  // Company information
  public string? CompanyName { get; set; }

  // Address
  public string Street { get; set; } = string.Empty;
  public string City { get; set; } = string.Empty;
  public string State { get; set; } = string.Empty;
  public string PostalCode { get; set; } = string.Empty;
  public string Country { get; set; } = string.Empty;
  public decimal? Latitude { get; set; }
  public decimal? Longitude { get; set; }

  // Customer specific fields
  public string? Notes { get; set; }
  public bool IsVip { get; set; }
  public decimal? CreditLimit { get; set; }
  public decimal CurrentBalance { get; set; }

  // Statistics
  public int TotalJobs { get; set; }
  public int CompletedJobs { get; set; }
  public int PendingJobs { get; set; }
  public int TotalInvoices { get; set; }
  public int UnpaidInvoices { get; set; }
  public decimal TotalSpent { get; set; }
  public double? AverageRating { get; set; }

  // Audit fields
  public DateTime CreatedAt { get; set; }
  public DateTime? LastModifiedAt { get; set; }
  public bool IsActive { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Domain.Entities.Customer, CustomerDto>()
        .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.User.FullName.FirstName))
        .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.User.FullName.LastName))
        .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.User.FullName.GetFullName()))
        .ForMember(d => d.Email, opt => opt.MapFrom(s => s.User.Email.Value))
        .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.User.PhoneNumber.ToString()))
        .ForMember(d => d.Street, opt => opt.MapFrom(s => s.Address.Street))
        .ForMember(d => d.City, opt => opt.MapFrom(s => s.Address.City))
        .ForMember(d => d.State, opt => opt.MapFrom(s => s.Address.State))
        .ForMember(d => d.PostalCode, opt => opt.MapFrom(s => s.Address.PostalCode))
        .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Address.Country))
        .ForMember(d => d.Latitude, opt => opt.MapFrom(s => s.Address.Coordinates != null ? s.Address.Coordinates.Latitude : (decimal?)null))
        .ForMember(d => d.Longitude, opt => opt.MapFrom(s => s.Address.Coordinates != null ? s.Address.Coordinates.Longitude : (decimal?)null))
        .ForMember(d => d.TotalJobs, opt => opt.MapFrom(s => s.Jobs.Count))
        .ForMember(d => d.CompletedJobs, opt => opt.MapFrom(s => s.Jobs.Count(j => j.CurrentStatus == Domain.Enums.JobStatusType.Completed || j.CurrentStatus == Domain.Enums.JobStatusType.Paid)))
        .ForMember(d => d.PendingJobs, opt => opt.MapFrom(s => s.Jobs.Count(j => j.CurrentStatus != Domain.Enums.JobStatusType.Completed && j.CurrentStatus != Domain.Enums.JobStatusType.Paid && j.CurrentStatus != Domain.Enums.JobStatusType.Cancelled)))
        .ForMember(d => d.TotalInvoices, opt => opt.MapFrom(s => s.Invoices.Count))
        .ForMember(d => d.UnpaidInvoices, opt => opt.MapFrom(s => s.Invoices.Count(i => i.PaymentStatus != Domain.Enums.PaymentStatus.Paid)))
        .ForMember(d => d.TotalSpent, opt => opt.MapFrom(s => s.Invoices.Where(i => i.PaymentStatus == Domain.Enums.PaymentStatus.Paid).Sum(i => i.TotalAmount.Amount)))
        .ForMember(d => d.AverageRating, opt => opt.MapFrom(s => s.Feedbacks.Any() ? s.Feedbacks.Average(f => (double)f.OverallRating) : (double?)null))
        .ForMember(d => d.IsActive, opt => opt.MapFrom(s => !s.IsDeleted));
  }
}
