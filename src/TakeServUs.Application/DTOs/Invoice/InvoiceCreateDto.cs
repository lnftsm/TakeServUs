using System.ComponentModel.DataAnnotations;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Application.DTOs.Invoice;

public class InvoiceCreateDto
{
  // Optional - if not provided, will be auto-generated
  public string? InvoiceNumber { get; set; }

  [Required(ErrorMessage = "Job ID is required")]
  public Guid JobId { get; set; }

  // Optional - will be retrieved from job if not provided
  public Guid? CustomerId { get; set; }

  // Optional - defaults to today
  public DateTime? InvoiceDate { get; set; }

  // Optional - defaults based on payment terms (e.g., 30 days)
  public DateTime? DueDate { get; set; }

  // Items
  [Required(ErrorMessage = "At least one invoice item is required")]
  [MinLength(1, ErrorMessage = "At least one invoice item is required")]
  public List<InvoiceItemCreateDto> Items { get; set; } = new();

  // Optional amounts - will be calculated if not provided
  public decimal? TaxRate { get; set; } // Percentage
  public decimal? DiscountRate { get; set; } // Percentage
  public decimal? DiscountAmount { get; set; } // Fixed amount

  // Optional - defaults to company currency
  public Currency? Currency { get; set; }

  // Additional information
  [MaxLength(1000, ErrorMessage = "Terms cannot exceed 1000 characters")]
  public string? Terms { get; set; }

  [MaxLength(2000, ErrorMessage = "Notes cannot exceed 2000 characters")]
  public string? Notes { get; set; }

  // Options
  public bool SendToCustomer { get; set; } = false;
  public bool CreateFromJobMaterials { get; set; } = false;
}

public class InvoiceItemCreateDto
{
  [Required(ErrorMessage = "Description is required")]
  [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
  public string Description { get; set; } = string.Empty;

  [Required(ErrorMessage = "Quantity is required")]
  [Range(0.001, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
  public decimal Quantity { get; set; }

  [Required(ErrorMessage = "Unit price is required")]
  [Range(0, double.MaxValue, ErrorMessage = "Unit price must be non-negative")]
  public decimal UnitPrice { get; set; }

  // Optional - will be calculated as Quantity * UnitPrice if not provided
  public decimal? TotalPrice { get; set; }

  [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
  public string? Notes { get; set; }

  // Optional reference to material
  public Guid? MaterialId { get; set; }
}
