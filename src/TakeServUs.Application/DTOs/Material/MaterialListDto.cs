using TakeServUs.Application.Common.Mappings;
using AutoMapper;

namespace TakeServUs.Application.DTOs.Material;

public class MaterialListDto : IMapFrom<Domain.Entities.Material>
{
  public Guid Id { get; set; }
  public string Code { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public string Unit { get; set; } = string.Empty;
  public decimal UnitCost { get; set; }
  public decimal? UnitPrice { get; set; }
  public decimal QuantityInStock { get; set; }
  public string? Category { get; set; }
  public bool IsLowStock { get; set; }
  public decimal StockValue { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Domain.Entities.Material, MaterialListDto>()
        .ForMember(d => d.UnitCost, opt => opt.MapFrom(s => s.UnitCost.Amount))
        .ForMember(d => d.UnitPrice, opt => opt.MapFrom(s => s.UnitPrice != null ? s.UnitPrice.Amount : (decimal?)null))
        .ForMember(d => d.IsLowStock, opt => opt.MapFrom(s => s.MinimumStock.HasValue && s.QuantityInStock <= s.MinimumStock.Value))
        .ForMember(d => d.StockValue, opt => opt.MapFrom(s => s.QuantityInStock * s.UnitCost.Amount));
  }
}
