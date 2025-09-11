using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Location;
using TakeServUs.Application.Interfaces.Services;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Application.Services;

public class LocationService : ILocationService
{
  public Task<ServiceResult<int>> DeleteOldLocationDataAsync(int daysToKeep = 30, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<double>> GetAverageResponseTimeAsync(Guid technicianId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<LocationDto>> GetCurrentLocationAsync(Guid technicianId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<double>> GetDistanceTraveledAsync(Guid technicianId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<TimeSpan>> GetDrivingTimeAsync(Guid technicianId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<LocationDto>>> GetLocationHistoryAsync(Guid technicianId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<LocationStatisticsDto>> GetLocationStatisticsAsync(Guid technicianId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<TechnicianNearbyDto>>> GetNearbyTechniciansAsync(GpsCoordinate location, double radiusKm, Guid? companyId = null, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<RouteDto>> GetRouteAsync(Guid technicianId, DateTime date, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> HasArrivedAtJobSiteAsync(Guid technicianId, Guid jobId, double thresholdMeters = 100, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> IsWithinServiceAreaAsync(GpsCoordinate location, Guid companyId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> PauseTrackingAsync(Guid technicianId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<LocationDto>>> RecordBatchLocationsAsync(IEnumerable<LocationCreateDto> locations, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<LocationDto>> RecordLocationAsync(LocationCreateDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> ResumeTrackingAsync(Guid technicianId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}
