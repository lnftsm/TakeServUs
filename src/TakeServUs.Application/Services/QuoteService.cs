using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Invoice;
using TakeServUs.Application.DTOs.Quote;
using TakeServUs.Application.Interfaces.Services;

namespace TakeServUs.Application.Services;

public class QuoteService : IQuoteService
{
  public Task<ServiceResult<bool>> AcceptQuoteAsync(Guid quoteId, string acceptedBy, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<InvoiceDto>> ConvertToInvoiceAsync(Guid quoteId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<QuoteDto>> CreateAsync(QuoteCreateDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<QuoteDto>> CreateFromJobAsync(Guid jobId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<QuoteDto>> DuplicateQuoteAsync(Guid quoteId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> ExtendValidityAsync(Guid quoteId, DateTime newValidUntil, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<byte[]>> GeneratePdfAsync(Guid quoteId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<string>> GenerateQuoteNumberAsync(Guid companyId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<QuoteDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<QuoteDto>> GetByJobIdAsync(Guid jobId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<QuoteListDto>>> GetExpiringQuotesAsync(int daysAhead = 7, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<PagedResponse<QuoteListDto>>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<QuoteListDto>>> GetQuotesByCustomerAsync(Guid customerId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<QuoteStatisticsDto>> GetQuoteStatisticsAsync(Guid companyId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> RejectQuoteAsync(Guid quoteId, string reason, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> SendQuoteEmailAsync(Guid quoteId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<QuoteDto>> UpdateAsync(Guid id, QuoteUpdateDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}
