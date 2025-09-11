namespace TakeServUs.Application.DTOs.Quote;

public class QuoteStatisticsDto
{
  public Guid CompanyId { get; set; }
  public DateTime FromDate { get; set; }
  public DateTime ToDate { get; set; }

  // Volume metrics
  public int TotalQuotes { get; set; }
  public int AcceptedQuotes { get; set; }
  public int RejectedQuotes { get; set; }
  public int ExpiredQuotes { get; set; }
  public int PendingQuotes { get; set; }

  // Conversion metrics
  public double AcceptanceRate { get; set; }
  public double ConversionRate { get; set; } // Quotes to invoices
  public double ExpirationRate { get; set; }
  public TimeSpan AverageTimeToAcceptance { get; set; }

  // Value metrics
  public decimal TotalQuoteValue { get; set; }
  public decimal AcceptedQuoteValue { get; set; }
  public decimal AverageQuoteValue { get; set; }
  public decimal MedianQuoteValue { get; set; }
  public string Currency { get; set; } = "TRY";

  // Performance metrics
  public TimeSpan AverageValidityPeriod { get; set; }
  public double AverageItemsPerQuote { get; set; }
  public double AverageDiscountPercentage { get; set; }

  // Customer metrics
  public int UniqueCustomers { get; set; }
  public double AverageQuotesPerCustomer { get; set; }
  public List<TopCustomerQuoteDto> TopCustomers { get; set; } = new();

  // Time-based breakdown
  public List<MonthlyQuoteStatisticsDto> MonthlyBreakdown { get; set; } = new();
  public List<QuoteByStatusDto> StatusBreakdown { get; set; } = new();

  // Reasons for rejection/expiration
  public List<RejectionReasonDto> RejectionReasons { get; set; } = new();
}

public class TopCustomerQuoteDto
{
  public Guid CustomerId { get; set; }
  public string CustomerName { get; set; } = string.Empty;
  public int QuoteCount { get; set; }
  public decimal TotalValue { get; set; }
  public double AcceptanceRate { get; set; }
}

public class MonthlyQuoteStatisticsDto
{
  public int Year { get; set; }
  public int Month { get; set; }
  public int QuoteCount { get; set; }
  public decimal TotalValue { get; set; }
  public double AcceptanceRate { get; set; }
}

public class QuoteByStatusDto
{
  public string Status { get; set; } = string.Empty;
  public int Count { get; set; }
  public decimal TotalValue { get; set; }
  public double Percentage { get; set; }
}

public class RejectionReasonDto
{
  public string Reason { get; set; } = string.Empty;
  public int Count { get; set; }
  public double Percentage { get; set; }
}
