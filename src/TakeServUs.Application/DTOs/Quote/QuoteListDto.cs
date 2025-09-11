using TakeServUs.Application.Common.Mappings;
using AutoMapper;

namespace TakeServUs.Application.DTOs.Quote;

public class QuoteListDto : IMapFrom<Domain.Entities.Quote>
{
  public Guid Id { get; set; }
  public string QuoteNumber { get; set; } = string.Empty;
  public string JobNumber { get; set; } = string.Empty;
  public string CustomerName { get; set; } = string.Empty;
  public DateTime QuoteDate { get; set; }
  public DateTime ValidUntil { get; set; }
  public decimal TotalAmount { get; set; }
  public string Currency { get; set; } = "TRY";
  public bool IsAccepted { get; set; }
  public bool IsExpired { get; set; }
  public bool HasInvoice { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Domain.Entities.Quote, QuoteListDto>()
        .ForMember(d => d.QuoteNumber, opt => opt.MapFrom(s => s.QuoteNumber))
        .ForMember(d => d.JobNumber, opt => opt.MapFrom(s => s.Job.JobNumber))
        .ForMember(d => d.CustomerName, opt => opt.MapFrom(s => s.Job.Customer.User.FullName.GetFullName()))
        .ForMember(d => d.TotalAmount, opt => opt.MapFrom(s => s.TotalAmount.Amount))
        .ForMember(d => d.Currency, opt => opt.MapFrom(s => s.TotalAmount.Currency.ToString()))
        .ForMember(d => d.IsExpired, opt => opt.MapFrom(s => DateTime.UtcNow > s.ValidUntil))
        .ForMember(d => d.HasInvoice, opt => opt.MapFrom(s => s.Job.Invoice != null));
  }
}
