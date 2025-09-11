using TakeServUs.Application.Common.Mappings;
using TakeServUs.Domain.Enums;
using AutoMapper;

namespace TakeServUs.Application.DTOs.Invoice;

public class InvoiceListDto : IMapFrom<Domain.Entities.Invoice>
{
  public Guid Id { get; set; }
  public string InvoiceNumber { get; set; } = string.Empty;
  public string JobNumber { get; set; } = string.Empty;
  public string CustomerName { get; set; } = string.Empty;
  public DateTime InvoiceDate { get; set; }
  public DateTime DueDate { get; set; }
  public decimal TotalAmount { get; set; }
  public decimal PaidAmount { get; set; }
  public decimal BalanceDue { get; set; }
  public string Currency { get; set; } = "TRY";
  public PaymentStatus PaymentStatus { get; set; }
  public bool IsSent { get; set; }
  public bool IsOverdue { get; set; }
  public int DaysOverdue { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Domain.Entities.Invoice, InvoiceListDto>()
        .ForMember(d => d.InvoiceNumber, opt => opt.MapFrom(s => s.InvoiceNumber.Value))
        .ForMember(d => d.JobNumber, opt => opt.MapFrom(s => s.Job.JobNumber))
        .ForMember(d => d.CustomerName, opt => opt.MapFrom(s => s.Customer.User.FullName.GetFullName()))
        .ForMember(d => d.TotalAmount, opt => opt.MapFrom(s => s.TotalAmount.Amount))
        .ForMember(d => d.PaidAmount, opt => opt.MapFrom(s => s.PaidAmount.Amount))
        .ForMember(d => d.BalanceDue, opt => opt.MapFrom(s => s.TotalAmount.Amount - s.PaidAmount.Amount))
        .ForMember(d => d.Currency, opt => opt.MapFrom(s => s.TotalAmount.Currency.ToString()))
        .ForMember(d => d.IsOverdue, opt => opt.MapFrom(s => s.PaymentStatus != PaymentStatus.Paid && DateTime.UtcNow > s.DueDate))
        .ForMember(d => d.DaysOverdue, opt => opt.MapFrom(s => s.PaymentStatus != PaymentStatus.Paid && DateTime.UtcNow > s.DueDate ? (DateTime.UtcNow - s.DueDate).Days : 0));
  }
}
