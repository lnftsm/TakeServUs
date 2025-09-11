using TakeServUs.Application.Common.Mappings;
using AutoMapper;

namespace TakeServUs.Application.DTOs.Invoice;

public class InvoiceItemDto : IMapFrom<Domain.Entities.InvoiceItem>
{
  public Guid Id { get; set; }
  public Guid InvoiceId { get; set; }
  public string Description { get; set; } = string.Empty;
  public decimal Quantity { get; set; }
  public decimal UnitPrice { get; set; }
  public decimal TotalPrice { get; set; }
  public string? Notes { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Domain.Entities.InvoiceItem, InvoiceItemDto>()
        .ForMember(d => d.UnitPrice, opt => opt.MapFrom(s => s.UnitPrice.Amount))
        .ForMember(d => d.TotalPrice, opt => opt.MapFrom(s => s.TotalPrice.Amount));
  }
}
