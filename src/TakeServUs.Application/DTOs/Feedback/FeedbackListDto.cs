using TakeServUs.Application.Common.Mappings;
using TakeServUs.Domain.Enums;
using AutoMapper;

namespace TakeServUs.Application.DTOs.Feedback;

public class FeedbackListDto : IMapFrom<Domain.Entities.Feedback>
{
  public Guid Id { get; set; }
  public Guid JobId { get; set; }
  public string JobNumber { get; set; } = string.Empty;
  public string CustomerName { get; set; } = string.Empty;
  public string? TechnicianName { get; set; }
  public FeedbackRating OverallRating { get; set; }
  public string? CommentPreview { get; set; }
  public bool HasResponse { get; set; }
  public bool IsPublic { get; set; }
  public DateTime CreatedAt { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Domain.Entities.Feedback, FeedbackListDto>()
        .ForMember(d => d.JobNumber, opt => opt.MapFrom(s => s.Job.JobNumber))
        .ForMember(d => d.CustomerName, opt => opt.MapFrom(s => s.Customer.User.FullName.GetFullName()))
        .ForMember(d => d.TechnicianName, opt => opt.MapFrom(s => s.Job.JobAssignments.FirstOrDefault(ja => ja.IsPrimary) != null ? s.Job.JobAssignments.FirstOrDefault(ja => ja.IsPrimary)!.Technician.User.FullName.GetFullName() : null))
        .ForMember(d => d.CommentPreview, opt => opt.MapFrom(s => string.IsNullOrEmpty(s.Comments) ? null : s.Comments.Length > 100 ? s.Comments.Substring(0, 100) + "..." : s.Comments))
        .ForMember(d => d.HasResponse, opt => opt.MapFrom(s => !string.IsNullOrEmpty(s.Response)));
  }
}
