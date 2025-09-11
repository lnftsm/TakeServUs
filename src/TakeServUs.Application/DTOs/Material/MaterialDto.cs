using TakeServUs.Application.Common.Mappings;
using AutoMapper;

namespace TakeServUs.Application.DTOs.Material;

public class MaterialDto : IMapFrom<Domain.Entities.Material>
{
  public Guid Id { get; set; }
  public Guid CompanyId { get; set; }
  public string Code { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public string? Description { get; set; }
  public string Unit { get; set; } = string.Empty;
  public decimal UnitCost { get; set; }
  public decimal? UnitPrice { get; set; }
  public string Currency { get; set; } = "TRY";
  public decimal QuantityInStock { get; set; }
  public decimal? MinimumStock { get; set; }
  public string? Category { get; set; }
  public string? Supplier { get; set; }
  public string? ImageUrl { get; set; }

  // Calculated fields
  public bool IsLowStock { get; set; }
  public decimal StockValue { get; set; }
  public int TimesUsed { get; set; }
  public decimal TotalQuantityUsed { get; set; }
  public DateTime? LastUsedDate { get; set; }

  // Audit fields
  public DateTime CreatedAt { get; set; }
  public DateTime? LastModifiedAt { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Domain.Entities.Material, MaterialDto>()
        .ForMember(d => d.UnitCost, opt => opt.MapFrom(s => s.UnitCost.Amount))
        .ForMember(d => d.UnitPrice, opt => opt.MapFrom(s => s.UnitPrice != null ? s.UnitPrice.Amount : (decimal?)null))
        .ForMember(d => d.Currency, opt => opt.MapFrom(s => s.UnitCost.Currency.ToString()))
        .ForMember(d => d.IsLowStock, opt => opt.MapFrom(s => s.MinimumStock.HasValue && s.QuantityInStock <= s.MinimumStock.Value))
        .ForMember(d => d.StockValue, opt => opt.MapFrom(s => s.QuantityInStock * s.UnitCost.Amount))
        .ForMember(d => d.TimesUsed, opt => opt.MapFrom(s => s.JobMaterials.Count))
        .ForMember(d => d.TotalQuantityUsed, opt => opt.MapFrom(s => s.JobMaterials.Sum(jm => jm.Quantity)))
        .ForMember(d => d.LastUsedDate, opt => opt.MapFrom(s => s.JobMaterials.OrderByDescending(jm => jm.UsedAt).FirstOrDefault() != null ? s.JobMaterials.OrderByDescending(jm => jm.UsedAt).First().UsedAt : (DateTime?)null));
  }
}
