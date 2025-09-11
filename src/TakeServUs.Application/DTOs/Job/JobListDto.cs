using TakeServUs.Application.Common.Mappings;
using TakeServUs.Domain.Enums;
using AutoMapper;

namespace TakeServUs.Application.DTOs.Job;

public class JobListDto : IMapFrom<Domain.Entities.Job>
{
  public Guid Id { get; set; }
  public string JobNumber { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
  public string CustomerName { get; set; } = string.Empty;
  public string ServiceCity { get; set; } = string.Empty;
  public JobPriority Priority { get; set; }
  public JobStatusType CurrentStatus { get; set; }
  public DateTime? ScheduledStartTime { get; set; }
  public string? PrimaryTechnicianName { get; set; }
  public decimal? EstimatedCost { get; set; }
  public string Currency { get; set; } = "TRY";
  public bool IsOverdue { get; set; }
  public DateTime CreatedAt { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Domain.Entities.Job, JobListDto>()
        .ForMember(d => d.CustomerName, opt => opt.MapFrom(s => s.Customer.User.FullName.GetFullName()))
        .ForMember(d => d.ServiceCity, opt => opt.MapFrom(s => s.ServiceAddress.City))
        .ForMember(d => d.PrimaryTechnicianName, opt => opt.MapFrom(s => s.JobAssignments.FirstOrDefault(ja => ja.IsPrimary) != null ? s.JobAssignments.FirstOrDefault(ja => ja.IsPrimary)!.Technician.User.FullName.GetFullName() : null))
        .ForMember(d => d.EstimatedCost, opt => opt.MapFrom(s => s.EstimatedCost != null ? s.EstimatedCost.Amount : (decimal?)null))
        .ForMember(d => d.Currency, opt => opt.MapFrom(s => s.EstimatedCost != null ? s.EstimatedCost.Currency.ToString() : "TRY"))
        .ForMember(d => d.IsOverdue, opt => opt.MapFrom(s => s.ScheduledEndTime.HasValue && DateTime.UtcNow > s.ScheduledEndTime.Value && s.CurrentStatus != JobStatusType.Completed && s.CurrentStatus != JobStatusType.Cancelled));
  }
}
