namespace TakeServUs.Application.DTOs.Material;

public class MaterialStockReportDto
{
  public Guid CompanyId { get; set; }
  public string CompanyName { get; set; } = string.Empty;
  public DateTime GeneratedAt { get; set; }

  // Summary statistics
  public int TotalMaterials { get; set; }
  public int LowStockMaterials { get; set; }
  public int OutOfStockMaterials { get; set; }
  public decimal TotalStockValue { get; set; }
  public string Currency { get; set; } = "TRY";

  // Category breakdown
  public List<CategoryStockDto> CategoryBreakdown { get; set; } = new();

  // Supplier breakdown
  public List<SupplierStockDto> SupplierBreakdown { get; set; } = new();

  // Low stock items
  public List<LowStockItemDto> LowStockItems { get; set; } = new();

  // Most used materials
  public List<MostUsedMaterialDto> MostUsedMaterials { get; set; } = new();

  // Recent movements
  public List<StockMovementDto> RecentMovements { get; set; } = new();

  // Recommendations
  public List<string> Recommendations { get; set; } = new();
}

public class CategoryStockDto
{
  public string Category { get; set; } = string.Empty;
  public int ItemCount { get; set; }
  public decimal TotalValue { get; set; }
  public int LowStockCount { get; set; }
}

public class SupplierStockDto
{
  public string Supplier { get; set; } = string.Empty;
  public int ItemCount { get; set; }
  public decimal TotalValue { get; set; }
}

public class LowStockItemDto
{
  public Guid MaterialId { get; set; }
  public string Code { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public decimal CurrentStock { get; set; }
  public decimal MinimumStock { get; set; }
  public decimal DeficitQuantity { get; set; }
  public decimal EstimatedOrderCost { get; set; }
}

public class MostUsedMaterialDto
{
  public Guid MaterialId { get; set; }
  public string Code { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public decimal TotalQuantityUsed { get; set; }
  public int TimesUsed { get; set; }
  public decimal AverageUsagePerJob { get; set; }
  public int EstimatedDaysUntilDepletion { get; set; }
}

public class StockMovementDto
{
  public DateTime Date { get; set; }
  public string MaterialName { get; set; } = string.Empty;
  public string MovementType { get; set; } = string.Empty; // In, Out, Adjustment
  public decimal Quantity { get; set; }
  public string? Reference { get; set; } // Job number or purchase order
  public string? Notes { get; set; }
}
