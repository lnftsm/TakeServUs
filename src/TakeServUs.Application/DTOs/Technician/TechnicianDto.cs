using TakeServUs.Application.Common.Mappings;
using AutoMapper;

namespace TakeServUs.Application.DTOs.Technician;

public class TechnicianDto : IMapFrom<Domain.Entities.Technician>
{
  public Guid Id { get; set; }
  public Guid UserId { get; set; }
  public Guid CompanyId { get; set; }

  // User information
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string FullName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;
  public string? ProfileImageUrl { get; set; }

  // Technician specific
  public string? EmployeeCode { get; set; }
  public string? Specialization { get; set; }
  public List<SkillDto> Skills { get; set; } = new();
  public bool IsAvailable { get; set; }
  public double? Rating { get; set; }
  public int TotalJobsCompleted { get; set; }

  // Current location
  public decimal? CurrentLatitude { get; set; }
  public decimal? CurrentLongitude { get; set; }
  public DateTime? LastLocationUpdate { get; set; }

  // Working schedule
  public List<DailyScheduleDto> WorkingSchedule { get; set; } = new();

  // Vehicle information
  public string? VehicleType { get; set; }
  public string? VehiclePlateNumber { get; set; }

  // Current status
  public int ActiveJobs { get; set; }
  public int PendingJobs { get; set; }
  public int CompletedJobsToday { get; set; }
  public string? CurrentJobNumber { get; set; }
  public string? CurrentJobLocation { get; set; }

  // Performance metrics
  public double? AverageResponseTime { get; set; }
  public double? AverageJobDuration { get; set; }
  public double? CustomerSatisfaction { get; set; }

  // Audit fields
  public DateTime CreatedAt { get; set; }
  public DateTime? LastModifiedAt { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Domain.Entities.Technician, TechnicianDto>()
        .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.User.FullName.FirstName))
        .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.User.FullName.LastName))
        .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.User.FullName.GetFullName()))
        .ForMember(d => d.Email, opt => opt.MapFrom(s => s.User.Email.Value))
        .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.User.PhoneNumber.ToString()))
        .ForMember(d => d.ProfileImageUrl, opt => opt.MapFrom(s => s.User.ProfileImageUrl))
        .ForMember(d => d.CurrentLatitude, opt => opt.MapFrom(s => s.CurrentLocation != null ? s.CurrentLocation.Latitude : (decimal?)null))
        .ForMember(d => d.CurrentLongitude, opt => opt.MapFrom(s => s.CurrentLocation != null ? s.CurrentLocation.Longitude : (decimal?)null))
        .ForMember(d => d.LastLocationUpdate, opt => opt.MapFrom(s => s.CurrentLocation != null ? s.CurrentLocation.Timestamp : (DateTime?)null))
        .ForMember(d => d.ActiveJobs, opt => opt.MapFrom(s => s.JobAssignments.Count(ja => ja.AcceptedAt.HasValue && !ja.CompletedAt.HasValue)))
        .ForMember(d => d.PendingJobs, opt => opt.MapFrom(s => s.JobAssignments.Count(ja => !ja.AcceptedAt.HasValue && !ja.DeclinedAt.HasValue)))
        .ForMember(d => d.CompletedJobsToday, opt => opt.MapFrom(s => s.JobAssignments.Count(ja => ja.CompletedAt.HasValue && ja.CompletedAt.Value.Date == DateTime.UtcNow.Date)));
  }
}

public class SkillDto
{
  public string Name { get; set; } = string.Empty;
  public string Level { get; set; } = string.Empty; // Beginner, Intermediate, Expert, Master
}

public class DailyScheduleDto
{
  public DayOfWeek Day { get; set; }
  public TimeOnly StartTime { get; set; }
  public TimeOnly EndTime { get; set; }
  public TimeSpan Duration { get; set; }
}
