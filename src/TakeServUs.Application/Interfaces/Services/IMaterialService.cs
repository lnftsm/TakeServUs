using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Material;

namespace TakeServUs.Application.Interfaces.Services;

public interface IMaterialService
{
  Task<ServiceResult<MaterialDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<ServiceResult<PagedResponse<MaterialListDto>>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default);
  Task<ServiceResult<MaterialDto>> CreateAsync(MaterialCreateDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<MaterialDto>> UpdateAsync(Guid id, MaterialUpdateDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

  // Material specific operations
  Task<ServiceResult<MaterialDto>> GetByCodeAsync(string code, Guid companyId, CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<MaterialListDto>>> GetLowStockMaterialsAsync(Guid companyId, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> UpdateStockAsync(Guid materialId, decimal quantity, string operation, string? notes = null, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> AdjustStockForJobAsync(Guid jobId, Guid materialId, decimal quantity, CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<MaterialListDto>>> GetByCategoryAsync(string category, Guid companyId, CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<MaterialUsageDto>>> GetUsageHistoryAsync(Guid materialId, DateTime from, DateTime to, CancellationToken cancellationToken = default);
  Task<ServiceResult<MaterialStockReportDto>> GetStockReportAsync(Guid companyId, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> ImportMaterialsAsync(Stream csvStream, Guid companyId, CancellationToken cancellationToken = default);
  Task<ServiceResult<byte[]>> ExportMaterialsAsync(Guid companyId, string format = "csv", CancellationToken cancellationToken = default);
  Task<ServiceResult<decimal>> CalculateMaterialCostForJobAsync(Guid jobId, CancellationToken cancellationToken = default);
}
