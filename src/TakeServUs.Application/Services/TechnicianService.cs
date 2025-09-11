using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Technician;
using TakeServUs.Application.Interfaces.Services;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Application.Services;

public class TechnicianService : ITechnicianService
{
  public Task<ServiceResult<TechnicianDto>> CreateAsync(TechnicianCreateDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<TechnicianListDto>>> GetAvailableTechniciansAsync(CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<TechnicianDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<TechnicianLocationDto>> GetCurrentLocationAsync(Guid technicianId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<TechnicianLocationDto>>> GetLocationHistoryAsync(Guid technicianId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<TechnicianListDto>>> GetNearbyTechniciansAsync(GpsCoordinate location, double radiusKm, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<PagedResponse<TechnicianListDto>>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<TechnicianDto>> UpdateAsync(Guid id, TechnicianUpdateDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> UpdateAvailabilityAsync(Guid technicianId, bool isAvailable, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> UpdateLocationAsync(Guid technicianId, TechnicianLocationDto location, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> UpdateScheduleAsync(Guid technicianId, List<DailySchedule> schedule, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}
