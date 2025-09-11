using TakeServUs.Application.Common.Mappings;
using TakeServUs.Domain.Enums;
using AutoMapper;

namespace TakeServUs.Application.DTOs.Feedback;

public class FeedbackDto : IMapFrom<Domain.Entities.Feedback>
{
  public Guid Id { get; set; }
  public Guid JobId { get; set; }
  public Guid CustomerId { get; set; }

  // Job information
  public string JobNumber { get; set; } = string.Empty;
  public string JobTitle { get; set; } = string.Empty;
  public DateTime? JobCompletedDate { get; set; }

  // Customer information
  public string CustomerName { get; set; } = string.Empty;
  public string CustomerEmail { get; set; } = string.Empty;

  // Technician information
  public Guid? TechnicianId { get; set; }
  public string? TechnicianName { get; set; }

  // Ratings
  public FeedbackRating OverallRating { get; set; }
  public FeedbackRating? QualityRating { get; set; }
  public FeedbackRating? PunctualityRating { get; set; }
  public FeedbackRating? ProfessionalismRating { get; set; }
  public FeedbackRating? ValueRating { get; set; }

  // Calculated average (excluding overall)
  public double? DetailedRatingAverage { get; set; }

  // Comments
  public string? Comments { get; set; }
  public string? Response { get; set; }
  public DateTime? RespondedAt { get; set; }
  public string? RespondedBy { get; set; }

  // Visibility
  public bool IsPublic { get; set; }

  // Audit fields
  public DateTime CreatedAt { get; set; }
  public DateTime? LastModifiedAt { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Domain.Entities.Feedback, FeedbackDto>()
        .ForMember(d => d.JobNumber, opt => opt.MapFrom(s => s.Job.JobNumber))
        .ForMember(d => d.JobTitle, opt => opt.MapFrom(s => s.Job.Title))
        .ForMember(d => d.JobCompletedDate, opt => opt.MapFrom(s => s.Job.ActualEndTime))
        .ForMember(d => d.CustomerName, opt => opt.MapFrom(s => s.Customer.User.FullName.GetFullName()))
        .ForMember(d => d.CustomerEmail, opt => opt.MapFrom(s => s.Customer.User.Email.Value))
        .ForMember(d => d.TechnicianId, opt => opt.MapFrom(s => s.Job.JobAssignments.FirstOrDefault(ja => ja.IsPrimary) != null ? s.Job.JobAssignments.FirstOrDefault(ja => ja.IsPrimary)!.TechnicianId : (Guid?)null))
        .ForMember(d => d.TechnicianName, opt => opt.MapFrom(s => s.Job.JobAssignments.FirstOrDefault(ja => ja.IsPrimary) != null ? s.Job.JobAssignments.FirstOrDefault(ja => ja.IsPrimary)!.Technician.User.FullName.GetFullName() : null))
        .ForMember(d => d.DetailedRatingAverage, opt => opt.MapFrom(s => CalculateDetailedAverage(s)));
  }

  private static double? CalculateDetailedAverage(Domain.Entities.Feedback feedback)
  {
    var ratings = new List<int>();
    if (feedback.QualityRating.HasValue) ratings.Add((int)feedback.QualityRating.Value);
    if (feedback.PunctualityRating.HasValue) ratings.Add((int)feedback.PunctualityRating.Value);
    if (feedback.ProfessionalismRating.HasValue) ratings.Add((int)feedback.ProfessionalismRating.Value);
    if (feedback.ValueRating.HasValue) ratings.Add((int)feedback.ValueRating.Value);

    return ratings.Any() ? ratings.Average() : null;
  }
}
