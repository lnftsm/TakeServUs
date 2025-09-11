using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Customer;
using TakeServUs.Application.Interfaces.Services;

namespace TakeServUs.Application.Services;

public class CustomerService : ICustomerService
{
  // Implementation of customer-related methods
  public Task<ServiceResult<CustomerDto>> CreateAsync(CustomerCreateDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<decimal>> GetBalanceAsync(Guid customerId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<CustomerDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<CustomerDto>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<PagedResponse<CustomerListDto>>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> MarkAsVipAsync(Guid customerId, bool isVip, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<CustomerListDto>>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<CustomerDto>> UpdateAsync(Guid id, CustomerUpdateDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> UpdateCreditLimitAsync(Guid customerId, decimal creditLimit, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}
