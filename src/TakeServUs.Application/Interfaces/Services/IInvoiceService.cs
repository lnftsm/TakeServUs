using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Invoice;
using TakeServUs.Domain.Enums;

namespace TakeServUs.Application.Interfaces.Services;

public interface IInvoiceService
{
  Task<ServiceResult<InvoiceDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<ServiceResult<PagedResponse<InvoiceListDto>>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default);
  Task<ServiceResult<InvoiceDto>> CreateAsync(InvoiceCreateDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<InvoiceDto>> UpdateAsync(Guid id, InvoiceUpdateDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

  // Invoice specific operations
  Task<ServiceResult<InvoiceDto>> CreateFromJobAsync(Guid jobId, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> MarkAsPaidAsync(Guid invoiceId, PaymentMethod paymentMethod, string? reference, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> MarkAsSentAsync(Guid invoiceId, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> SendInvoiceEmailAsync(Guid invoiceId, CancellationToken cancellationToken = default);
  Task<ServiceResult<byte[]>> GeneratePdfAsync(Guid invoiceId, CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<InvoiceListDto>>> GetOverdueInvoicesAsync(CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<InvoiceListDto>>> GetInvoicesByCustomerAsync(Guid customerId, CancellationToken cancellationToken = default);
  Task<ServiceResult<string>> GenerateNextInvoiceNumberAsync(CancellationToken cancellationToken = default);
}
