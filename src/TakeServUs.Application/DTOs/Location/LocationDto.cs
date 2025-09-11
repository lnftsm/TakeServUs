using TakeServUs.Application.Common.Mappings;
using AutoMapper;

namespace TakeServUs.Application.DTOs.Location;

public class LocationDto : IMapFrom<Domain.Entities.Location>
{
  public Guid Id { get; set; }
  public Guid TechnicianId { get; set; }
  public string TechnicianName { get; set; } = string.Empty;
  public decimal Latitude { get; set; }
  public decimal Longitude { get; set; }
  public DateTime Timestamp { get; set; }
  public decimal? Accuracy { get; set; } // in meters
  public double? Speed { get; set; } // in km/h
  public double? Heading { get; set; } // in degrees
  public string? ActivityType { get; set; } // Driving, Walking, Stationary
  public DateTime RecordedAt { get; set; }

  // Calculated fields
  public double? DistanceFromPrevious { get; set; } // in meters
  public TimeSpan? TimeSincePrevious { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Domain.Entities.Location, LocationDto>()
        .ForMember(d => d.TechnicianName, opt => opt.MapFrom(s => s.Technician.User.FullName.GetFullName()))
        .ForMember(d => d.Latitude, opt => opt.MapFrom(s => s.Coordinate.Latitude))
        .ForMember(d => d.Longitude, opt => opt.MapFrom(s => s.Coordinate.Longitude))
        .ForMember(d => d.Timestamp, opt => opt.MapFrom(s => s.Coordinate.Timestamp))
        .ForMember(d => d.Accuracy, opt => opt.MapFrom(s => s.Coordinate.Accuracy))
        .ForMember(d => d.RecordedAt, opt => opt.MapFrom(s => s.RecordedAt));
  }
}
