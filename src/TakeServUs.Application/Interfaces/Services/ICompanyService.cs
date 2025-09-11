using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Company;

namespace TakeServUs.Application.Interfaces.Services;

public interface ICompanyService
{
  Task<ServiceResult<CompanyDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<ServiceResult<PagedResponse<CompanyListDto>>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default);
  Task<ServiceResult<CompanyDto>> CreateAsync(CompanyCreateDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<CompanyDto>> UpdateAsync(Guid id, CompanyUpdateDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> UpdateSubscriptionAsync(Guid id, string plan, DateTime expiryDate, CancellationToken cancellationToken = default);
  Task<ServiceResult<CompanyDto>> GetCurrentCompanyAsync(CancellationToken cancellationToken = default);
}
