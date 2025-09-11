using TakeServUs.Application.Common.Mappings;
using TakeServUs.Domain.Entities;
using AutoMapper;

namespace TakeServUs.Application.DTOs.Quote;

public class QuoteItemDto : IMapFrom<QuoteItem>
{
  public Guid Id { get; set; }
  public Guid QuoteId { get; set; }
  public string Description { get; set; } = string.Empty;
  public decimal Quantity { get; set; }
  public decimal UnitPrice { get; set; }
  public decimal TotalPrice { get; set; }
  public string? Notes { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<QuoteItem, QuoteItemDto>()
        .ForMember(d => d.UnitPrice, opt => opt.MapFrom(s => s.UnitPrice.Amount))
        .ForMember(d => d.TotalPrice, opt => opt.MapFrom(s => s.TotalPrice.Amount));
  }
}
