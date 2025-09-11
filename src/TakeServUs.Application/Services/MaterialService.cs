using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Material;
using TakeServUs.Application.Interfaces.Services;

namespace TakeServUs.Application.Services;

public class MaterialService : IMaterialService
{
  public Task<ServiceResult<bool>> AdjustStockForJobAsync(Guid jobId, Guid materialId, decimal quantity, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<decimal>> CalculateMaterialCostForJobAsync(Guid jobId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<MaterialDto>> CreateAsync(MaterialCreateDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<byte[]>> ExportMaterialsAsync(Guid companyId, string format = "csv", CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<MaterialListDto>>> GetByCategoryAsync(string category, Guid companyId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<MaterialDto>> GetByCodeAsync(string code, Guid companyId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<MaterialDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<MaterialListDto>>> GetLowStockMaterialsAsync(Guid companyId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<PagedResponse<MaterialListDto>>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<MaterialStockReportDto>> GetStockReportAsync(Guid companyId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<MaterialUsageDto>>> GetUsageHistoryAsync(Guid materialId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> ImportMaterialsAsync(Stream csvStream, Guid companyId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<MaterialDto>> UpdateAsync(Guid id, MaterialUpdateDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> UpdateStockAsync(Guid materialId, decimal quantity, string operation, string? notes = null, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}
