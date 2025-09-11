using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Invoice;
using TakeServUs.Application.DTOs.Quote;

namespace TakeServUs.Application.Interfaces.Services;

public interface IQuoteService
{
  Task<ServiceResult<QuoteDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<ServiceResult<PagedResponse<QuoteListDto>>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default);
  Task<ServiceResult<QuoteDto>> CreateAsync(QuoteCreateDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<QuoteDto>> UpdateAsync(Guid id, QuoteUpdateDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

  // Quote specific operations
  Task<ServiceResult<QuoteDto>> CreateFromJobAsync(Guid jobId, CancellationToken cancellationToken = default);
  Task<ServiceResult<QuoteDto>> GetByJobIdAsync(Guid jobId, CancellationToken cancellationToken = default);
  Task<ServiceResult<string>> GenerateQuoteNumberAsync(Guid companyId, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> AcceptQuoteAsync(Guid quoteId, string acceptedBy, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> RejectQuoteAsync(Guid quoteId, string reason, CancellationToken cancellationToken = default);
  Task<ServiceResult<QuoteDto>> DuplicateQuoteAsync(Guid quoteId, CancellationToken cancellationToken = default);
  Task<ServiceResult<InvoiceDto>> ConvertToInvoiceAsync(Guid quoteId, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> SendQuoteEmailAsync(Guid quoteId, CancellationToken cancellationToken = default);
  Task<ServiceResult<byte[]>> GeneratePdfAsync(Guid quoteId, CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<QuoteListDto>>> GetExpiringQuotesAsync(int daysAhead = 7, CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<QuoteListDto>>> GetQuotesByCustomerAsync(Guid customerId, CancellationToken cancellationToken = default);
  Task<ServiceResult<QuoteStatisticsDto>> GetQuoteStatisticsAsync(Guid companyId, DateTime from, DateTime to, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> ExtendValidityAsync(Guid quoteId, DateTime newValidUntil, CancellationToken cancellationToken = default);
}
