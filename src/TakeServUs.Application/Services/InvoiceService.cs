using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Invoice;
using TakeServUs.Application.Interfaces.Services;
using TakeServUs.Domain.Enums;

namespace TakeServUs.Application.Services;

public class InvoiceService : IInvoiceService
{
  public Task<ServiceResult<InvoiceDto>> CreateAsync(InvoiceCreateDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<InvoiceDto>> CreateFromJobAsync(Guid jobId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<string>> GenerateNextInvoiceNumberAsync(CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<byte[]>> GeneratePdfAsync(Guid invoiceId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<InvoiceDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<InvoiceListDto>>> GetInvoicesByCustomerAsync(Guid customerId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<InvoiceListDto>>> GetOverdueInvoicesAsync(CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<PagedResponse<InvoiceListDto>>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> MarkAsPaidAsync(Guid invoiceId, PaymentMethod paymentMethod, string? reference, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> MarkAsSentAsync(Guid invoiceId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> SendInvoiceEmailAsync(Guid invoiceId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<InvoiceDto>> UpdateAsync(Guid id, InvoiceUpdateDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}
