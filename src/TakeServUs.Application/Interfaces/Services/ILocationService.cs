using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Location;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Application.Interfaces.Services;

public interface ILocationService
{
  // Location tracking
  Task<ServiceResult<LocationDto>> RecordLocationAsync(LocationCreateDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<LocationDto>>> RecordBatchLocationsAsync(IEnumerable<LocationCreateDto> locations, CancellationToken cancellationToken = default);
  Task<ServiceResult<LocationDto>> GetCurrentLocationAsync(Guid technicianId, CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<LocationDto>>> GetLocationHistoryAsync(Guid technicianId, DateTime from, DateTime to, CancellationToken cancellationToken = default);

  // Route tracking
  Task<ServiceResult<RouteDto>> GetRouteAsync(Guid technicianId, DateTime date, CancellationToken cancellationToken = default);
  Task<ServiceResult<double>> GetDistanceTraveledAsync(Guid technicianId, DateTime from, DateTime to, CancellationToken cancellationToken = default);
  Task<ServiceResult<TimeSpan>> GetDrivingTimeAsync(Guid technicianId, DateTime from, DateTime to, CancellationToken cancellationToken = default);

  // Geofencing
  Task<ServiceResult<bool>> IsWithinServiceAreaAsync(GpsCoordinate location, Guid companyId, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> HasArrivedAtJobSiteAsync(Guid technicianId, Guid jobId, double thresholdMeters = 100, CancellationToken cancellationToken = default);

  // Analytics
  Task<ServiceResult<IEnumerable<TechnicianNearbyDto>>> GetNearbyTechniciansAsync(GpsCoordinate location, double radiusKm, Guid? companyId = null, CancellationToken cancellationToken = default);
  Task<ServiceResult<double>> GetAverageResponseTimeAsync(Guid technicianId, DateTime from, DateTime to, CancellationToken cancellationToken = default);
  Task<ServiceResult<LocationStatisticsDto>> GetLocationStatisticsAsync(Guid technicianId, DateTime from, DateTime to, CancellationToken cancellationToken = default);

  // Privacy and cleanup
  Task<ServiceResult<int>> DeleteOldLocationDataAsync(int daysToKeep = 30, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> PauseTrackingAsync(Guid technicianId, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> ResumeTrackingAsync(Guid technicianId, CancellationToken cancellationToken = default);
}
