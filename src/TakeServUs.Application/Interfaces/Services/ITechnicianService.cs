using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Technician;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Application.Interfaces.Services;

public interface ITechnicianService
{
  Task<ServiceResult<TechnicianDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<ServiceResult<PagedResponse<TechnicianListDto>>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default);
  Task<ServiceResult<TechnicianDto>> CreateAsync(TechnicianCreateDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<TechnicianDto>> UpdateAsync(Guid id, TechnicianUpdateDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

  // Technician specific operations
  Task<ServiceResult<bool>> UpdateLocationAsync(Guid technicianId, TechnicianLocationDto location, CancellationToken cancellationToken = default);
  Task<ServiceResult<TechnicianLocationDto>> GetCurrentLocationAsync(Guid technicianId, CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<TechnicianListDto>>> GetAvailableTechniciansAsync(CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<TechnicianListDto>>> GetNearbyTechniciansAsync(GpsCoordinate location, double radiusKm, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> UpdateAvailabilityAsync(Guid technicianId, bool isAvailable, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> UpdateScheduleAsync(Guid technicianId, List<DailySchedule> schedule, CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<TechnicianLocationDto>>> GetLocationHistoryAsync(Guid technicianId, DateTime from, DateTime to, CancellationToken cancellationToken = default);
}
