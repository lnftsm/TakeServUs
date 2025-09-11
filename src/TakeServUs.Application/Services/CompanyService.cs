using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Company;
using TakeServUs.Application.Interfaces.Services;

namespace TakeServUs.Application.Services;

public class CompanyService : ICompanyService
{
  // Implementation of company-related methods
  public Task<ServiceResult<CompanyDto>> CreateAsync(CompanyCreateDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<CompanyDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<CompanyDto>> GetCurrentCompanyAsync(CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<PagedResponse<CompanyListDto>>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<CompanyDto>> UpdateAsync(Guid id, CompanyUpdateDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> UpdateSubscriptionAsync(Guid id, string plan, DateTime expiryDate, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}
