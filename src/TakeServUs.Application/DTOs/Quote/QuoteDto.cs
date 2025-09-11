using TakeServUs.Application.Common.Mappings;
using AutoMapper;

namespace TakeServUs.Application.DTOs.Quote;

public class QuoteDto : IMapFrom<Domain.Entities.Quote>
{
  public Guid Id { get; set; }
  public string QuoteNumber { get; set; } = string.Empty;
  public Guid JobId { get; set; }

  // Job information
  public string JobNumber { get; set; } = string.Empty;
  public string JobTitle { get; set; } = string.Empty;
  public string JobDescription { get; set; } = string.Empty;

  // Customer information
  public Guid CustomerId { get; set; }
  public string CustomerName { get; set; } = string.Empty;
  public string CustomerEmail { get; set; } = string.Empty;
  public string CustomerPhone { get; set; } = string.Empty;
  public string? CustomerCompanyName { get; set; }

  // Customer address
  public string CustomerStreet { get; set; } = string.Empty;
  public string CustomerCity { get; set; } = string.Empty;
  public string CustomerState { get; set; } = string.Empty;
  public string CustomerPostalCode { get; set; } = string.Empty;
  public string CustomerCountry { get; set; } = string.Empty;

  // Dates
  public DateTime QuoteDate { get; set; }
  public DateTime ValidUntil { get; set; }

  // Amounts
  public decimal SubTotal { get; set; }
  public decimal? TaxAmount { get; set; }
  public decimal? DiscountAmount { get; set; }
  public decimal TotalAmount { get; set; }
  public string Currency { get; set; } = "TRY";

  // Items
  public List<QuoteItemDto> Items { get; set; } = new();

  // Additional information
  public string? Terms { get; set; }
  public string? Notes { get; set; }

  // Acceptance
  public bool IsAccepted { get; set; }
  public DateTime? AcceptedAt { get; set; }
  public string? AcceptedBy { get; set; }

  // Status flags
  public bool IsExpired { get; set; }
  public int DaysUntilExpiry { get; set; }
  public bool HasInvoice { get; set; }
  public Guid? InvoiceId { get; set; }

  // Audit fields
  public DateTime CreatedAt { get; set; }
  public DateTime? LastModifiedAt { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Domain.Entities.Quote, QuoteDto>()
        .ForMember(d => d.QuoteNumber, opt => opt.MapFrom(s => s.QuoteNumber))
        .ForMember(d => d.JobNumber, opt => opt.MapFrom(s => s.Job.JobNumber))
        .ForMember(d => d.JobTitle, opt => opt.MapFrom(s => s.Job.Title))
        .ForMember(d => d.JobDescription, opt => opt.MapFrom(s => s.Job.Description ?? string.Empty))
        .ForMember(d => d.CustomerId, opt => opt.MapFrom(s => s.Job.CustomerId))
        .ForMember(d => d.CustomerName, opt => opt.MapFrom(s => s.Job.Customer.User.FullName.GetFullName()))
        .ForMember(d => d.CustomerEmail, opt => opt.MapFrom(s => s.Job.Customer.User.Email.Value))
        .ForMember(d => d.CustomerPhone, opt => opt.MapFrom(s => s.Job.Customer.User.PhoneNumber.ToString()))
        .ForMember(d => d.CustomerCompanyName, opt => opt.MapFrom(s => s.Job.Customer.CompanyName))
        .ForMember(d => d.CustomerStreet, opt => opt.MapFrom(s => s.Job.Customer.Address.Street))
        .ForMember(d => d.CustomerCity, opt => opt.MapFrom(s => s.Job.Customer.Address.City))
        .ForMember(d => d.CustomerState, opt => opt.MapFrom(s => s.Job.Customer.Address.State))
        .ForMember(d => d.CustomerPostalCode, opt => opt.MapFrom(s => s.Job.Customer.Address.PostalCode))
        .ForMember(d => d.CustomerCountry, opt => opt.MapFrom(s => s.Job.Customer.Address.Country))
        .ForMember(d => d.SubTotal, opt => opt.MapFrom(s => s.SubTotal.Amount))
        .ForMember(d => d.TaxAmount, opt => opt.MapFrom(s => s.TaxAmount != null ? s.TaxAmount.Amount : (decimal?)null))
        .ForMember(d => d.DiscountAmount, opt => opt.MapFrom(s => s.DiscountAmount != null ? s.DiscountAmount.Amount : (decimal?)null))
        .ForMember(d => d.TotalAmount, opt => opt.MapFrom(s => s.TotalAmount.Amount))
        .ForMember(d => d.Currency, opt => opt.MapFrom(s => s.TotalAmount.Currency.ToString()))
        .ForMember(d => d.IsExpired, opt => opt.MapFrom(s => DateTime.UtcNow > s.ValidUntil))
        .ForMember(d => d.DaysUntilExpiry, opt => opt.MapFrom(s => (s.ValidUntil - DateTime.UtcNow).Days))
        .ForMember(d => d.HasInvoice, opt => opt.MapFrom(s => s.Job.Invoice != null))
        .ForMember(d => d.InvoiceId, opt => opt.MapFrom(s => s.Job.Invoice != null ? s.Job.Invoice.Id : (Guid?)null));
  }
}
