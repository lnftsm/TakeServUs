using System.ComponentModel.DataAnnotations;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Application.DTOs.Material;

public class MaterialCreateDto
{
  // Optional - will use current user's company if not provided
  public Guid? CompanyId { get; set; }

  [Required(ErrorMessage = "Material code is required")]
  [MaxLength(50, ErrorMessage = "Code cannot exceed 50 characters")]
  public string Code { get; set; } = string.Empty;

  [Required(ErrorMessage = "Material name is required")]
  [MaxLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
  public string Name { get; set; } = string.Empty;

  [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
  public string? Description { get; set; }

  [Required(ErrorMessage = "Unit is required")]
  [MaxLength(20, ErrorMessage = "Unit cannot exceed 20 characters")]
  public string Unit { get; set; } = string.Empty; // piece, meter, kg, liter, etc.

  [Required(ErrorMessage = "Unit cost is required")]
  [Range(0, double.MaxValue, ErrorMessage = "Unit cost must be non-negative")]
  public decimal UnitCost { get; set; }

  [Range(0, double.MaxValue, ErrorMessage = "Unit price must be non-negative")]
  public decimal? UnitPrice { get; set; }

  public Currency Currency { get; set; } = Currency.TRY;

  [Required(ErrorMessage = "Quantity in stock is required")]
  [Range(0, double.MaxValue, ErrorMessage = "Quantity must be non-negative")]
  public decimal QuantityInStock { get; set; }

  [Range(0, double.MaxValue, ErrorMessage = "Minimum stock must be non-negative")]
  public decimal? MinimumStock { get; set; }

  [MaxLength(100, ErrorMessage = "Category cannot exceed 100 characters")]
  public string? Category { get; set; }

  [MaxLength(200, ErrorMessage = "Supplier cannot exceed 200 characters")]
  public string? Supplier { get; set; }

  [Url(ErrorMessage = "Invalid image URL")]
  public string? ImageUrl { get; set; }
}
