using System.ComponentModel.DataAnnotations;

namespace TakeServUs.Application.DTOs.Quote;

public class QuoteUpdateDto
{
  public DateTime? ValidUntil { get; set; }

  public decimal? TaxRate { get; set; }
  public decimal? DiscountRate { get; set; }
  public decimal? DiscountAmount { get; set; }

  [MaxLength(1000, ErrorMessage = "Terms cannot exceed 1000 characters")]
  public string? Terms { get; set; }

  [MaxLength(2000, ErrorMessage = "Notes cannot exceed 2000 characters")]
  public string? Notes { get; set; }

  // Update items (replace all)
  public List<QuoteItemUpdateDto>? Items { get; set; }
}

public class QuoteItemUpdateDto
{
  public Guid? Id { get; set; } // For existing items

  [Required(ErrorMessage = "Description is required")]
  [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
  public string Description { get; set; } = string.Empty;

  [Required(ErrorMessage = "Quantity is required")]
  [Range(0.001, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
  public decimal Quantity { get; set; }

  [Required(ErrorMessage = "Unit price is required")]
  [Range(0, double.MaxValue, ErrorMessage = "Unit price must be non-negative")]
  public decimal UnitPrice { get; set; }

  public decimal? TotalPrice { get; set; }

  [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
  public string? Notes { get; set; }
}
