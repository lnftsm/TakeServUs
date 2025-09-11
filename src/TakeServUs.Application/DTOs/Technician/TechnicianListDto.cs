using TakeServUs.Application.Common.Mappings;
using AutoMapper;

namespace TakeServUs.Application.DTOs.Technician;

public class TechnicianListDto : IMapFrom<Domain.Entities.Technician>
{
  public Guid Id { get; set; }
  public Guid UserId { get; set; }
  public string FullName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;
  public string? EmployeeCode { get; set; }
  public string? Specialization { get; set; }
  public bool IsAvailable { get; set; }
  public double? Rating { get; set; }
  public int TotalJobsCompleted { get; set; }
  public int ActiveJobs { get; set; }
  public string? CurrentLocation { get; set; }
  public DateTime? LastLocationUpdate { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Domain.Entities.Technician, TechnicianListDto>()
        .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.User.FullName.GetFullName()))
        .ForMember(d => d.Email, opt => opt.MapFrom(s => s.User.Email.Value))
        .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.User.PhoneNumber.ToString()))
        .ForMember(d => d.ActiveJobs, opt => opt.MapFrom(s => s.JobAssignments.Count(ja => ja.AcceptedAt.HasValue && !ja.CompletedAt.HasValue)))
        .ForMember(d => d.CurrentLocation, opt => opt.MapFrom(s => s.CurrentLocation != null ? $"{s.CurrentLocation.Latitude}, {s.CurrentLocation.Longitude}" : null))
        .ForMember(d => d.LastLocationUpdate, opt => opt.MapFrom(s => s.CurrentLocation != null ? s.CurrentLocation.Timestamp : (DateTime?)null));
  }
}
