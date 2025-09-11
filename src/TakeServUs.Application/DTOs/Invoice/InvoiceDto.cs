using TakeServUs.Application.Common.Mappings;
using TakeServUs.Domain.Enums;
using AutoMapper;

namespace TakeServUs.Application.DTOs.Invoice;

public class InvoiceDto : IMapFrom<Domain.Entities.Invoice>
{
  public Guid Id { get; set; }
  public string InvoiceNumber { get; set; } = string.Empty;
  public Guid JobId { get; set; }
  public Guid CustomerId { get; set; }

  // Job information
  public string JobNumber { get; set; } = string.Empty;
  public string JobTitle { get; set; } = string.Empty;

  // Customer information
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
  public DateTime InvoiceDate { get; set; }
  public DateTime DueDate { get; set; }

  // Amounts
  public decimal SubTotal { get; set; }
  public decimal? TaxAmount { get; set; }
  public decimal? DiscountAmount { get; set; }
  public decimal TotalAmount { get; set; }
  public decimal PaidAmount { get; set; }
  public decimal BalanceDue { get; set; }
  public string Currency { get; set; } = "TRY";

  // Payment information
  public PaymentStatus PaymentStatus { get; set; }
  public PaymentMethod? PaymentMethod { get; set; }
  public DateTime? PaidAt { get; set; }
  public string? PaymentReference { get; set; }

  // Items
  public List<InvoiceItemDto> Items { get; set; } = new();

  // Additional information
  public string? Terms { get; set; }
  public string? Notes { get; set; }

  // Status flags
  public bool IsSent { get; set; }
  public DateTime? SentAt { get; set; }
  public bool IsOverdue { get; set; }
  public int DaysOverdue { get; set; }

  // Audit fields
  public DateTime CreatedAt { get; set; }
  public DateTime? LastModifiedAt { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Domain.Entities.Invoice, InvoiceDto>()
        .ForMember(d => d.InvoiceNumber, opt => opt.MapFrom(s => s.InvoiceNumber.Value))
        .ForMember(d => d.JobNumber, opt => opt.MapFrom(s => s.Job.JobNumber))
        .ForMember(d => d.JobTitle, opt => opt.MapFrom(s => s.Job.Title))
        .ForMember(d => d.CustomerName, opt => opt.MapFrom(s => s.Customer.User.FullName.GetFullName()))
        .ForMember(d => d.CustomerEmail, opt => opt.MapFrom(s => s.Customer.User.Email.Value))
        .ForMember(d => d.CustomerPhone, opt => opt.MapFrom(s => s.Customer.User.PhoneNumber.ToString()))
        .ForMember(d => d.CustomerCompanyName, opt => opt.MapFrom(s => s.Customer.CompanyName))
        .ForMember(d => d.CustomerStreet, opt => opt.MapFrom(s => s.Customer.Address.Street))
        .ForMember(d => d.CustomerCity, opt => opt.MapFrom(s => s.Customer.Address.City))
        .ForMember(d => d.CustomerState, opt => opt.MapFrom(s => s.Customer.Address.State))
        .ForMember(d => d.CustomerPostalCode, opt => opt.MapFrom(s => s.Customer.Address.PostalCode))
        .ForMember(d => d.CustomerCountry, opt => opt.MapFrom(s => s.Customer.Address.Country))
        .ForMember(d => d.SubTotal, opt => opt.MapFrom(s => s.SubTotal.Amount))
        .ForMember(d => d.TaxAmount, opt => opt.MapFrom(s => s.TaxAmount != null ? s.TaxAmount.Amount : (decimal?)null))
        .ForMember(d => d.DiscountAmount, opt => opt.MapFrom(s => s.DiscountAmount != null ? s.DiscountAmount.Amount : (decimal?)null))
        .ForMember(d => d.TotalAmount, opt => opt.MapFrom(s => s.TotalAmount.Amount))
        .ForMember(d => d.PaidAmount, opt => opt.MapFrom(s => s.PaidAmount.Amount))
        .ForMember(d => d.BalanceDue, opt => opt.MapFrom(s => s.TotalAmount.Amount - s.PaidAmount.Amount))
        .ForMember(d => d.Currency, opt => opt.MapFrom(s => s.TotalAmount.Currency.ToString()))
        .ForMember(d => d.IsOverdue, opt => opt.MapFrom(s => s.PaymentStatus != PaymentStatus.Paid && DateTime.UtcNow > s.DueDate))
        .ForMember(d => d.DaysOverdue, opt => opt.MapFrom(s => s.PaymentStatus != PaymentStatus.Paid && DateTime.UtcNow > s.DueDate ? (DateTime.UtcNow - s.DueDate).Days : 0));
  }
}
