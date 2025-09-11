using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Customer;

namespace TakeServUs.Application.Interfaces.Services;

public interface ICustomerService
{
  Task<ServiceResult<CustomerDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<ServiceResult<PagedResponse<CustomerListDto>>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default);
  Task<ServiceResult<CustomerDto>> CreateAsync(CustomerCreateDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<CustomerDto>> UpdateAsync(Guid id, CustomerUpdateDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

  // Customer specific operations
  Task<ServiceResult<CustomerDto>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<CustomerListDto>>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> UpdateCreditLimitAsync(Guid customerId, decimal creditLimit, CancellationToken cancellationToken = default);
  Task<ServiceResult<decimal>> GetBalanceAsync(Guid customerId, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> MarkAsVipAsync(Guid customerId, bool isVip, CancellationToken cancellationToken = default);
}
