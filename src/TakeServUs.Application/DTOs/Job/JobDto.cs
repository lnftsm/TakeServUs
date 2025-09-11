using TakeServUs.Application.Common.Mappings;
using TakeServUs.Domain.Enums;
using AutoMapper;
using TakeServUs.Application.Common.Helpers;

namespace TakeServUs.Application.DTOs.Job;

public class JobDto : IMapFrom<Domain.Entities.Job>
{
  public Guid Id { get; set; }
  public string JobNumber { get; set; } = string.Empty;
  public Guid CompanyId { get; set; }
  public Guid CustomerId { get; set; }

  // Customer information
  public string CustomerName { get; set; } = string.Empty;
  public string CustomerEmail { get; set; } = string.Empty;
  public string CustomerPhone { get; set; } = string.Empty;
  public string? CustomerCompanyName { get; set; }

  // Job details
  public string Title { get; set; } = string.Empty;
  public string? Description { get; set; }
  public JobPriority Priority { get; set; }
  public JobStatusType CurrentStatus { get; set; }

  // Service location
  public string ServiceStreet { get; set; } = string.Empty;
  public string ServiceCity { get; set; } = string.Empty;
  public string ServiceState { get; set; } = string.Empty;
  public string ServicePostalCode { get; set; } = string.Empty;
  public string ServiceCountry { get; set; } = string.Empty;
  public decimal? ServiceLatitude { get; set; }
  public decimal? ServiceLongitude { get; set; }

  // Scheduling
  public DateTime? ScheduledStartTime { get; set; }
  public DateTime? ScheduledEndTime { get; set; }
  public DateTime? ActualStartTime { get; set; }
  public DateTime? ActualEndTime { get; set; }
  public int? EstimatedDurationMinutes { get; set; }
  public int? ActualDurationMinutes { get; set; }

  // Pricing
  public decimal? EstimatedCost { get; set; }
  public decimal? ActualCost { get; set; }
  public string Currency { get; set; } = "TRY";

  // Assignments
  public List<JobAssignmentDto> Assignments { get; set; } = new();
  public string? PrimaryTechnicianName { get; set; }
  public Guid? PrimaryTechnicianId { get; set; }

  // Materials
  public List<JobMaterialDto> Materials { get; set; } = new();
  public decimal TotalMaterialCost { get; set; }

  // Status history
  public List<JobStatusHistoryDto> StatusHistory { get; set; } = new();

  // Additional information
  public string? Notes { get; set; }
  public string? InternalNotes { get; set; }
  public List<string> PhotoUrls { get; set; } = new();
  public List<string> Attachments { get; set; } = new();
  public string? SignatureUrl { get; set; }
  public bool RequiresCustomerSignature { get; set; }

  // Recurrence
  public bool IsRecurring { get; set; }
  public RecurrencePatternDto? RecurrencePattern { get; set; }

  // Related entities
  public bool HasQuote { get; set; }
  public Guid? QuoteId { get; set; }
  public bool HasInvoice { get; set; }
  public Guid? InvoiceId { get; set; }
  public bool HasFeedback { get; set; }
  public Guid? FeedbackId { get; set; }

  // Audit fields
  public DateTime CreatedAt { get; set; }
  public DateTime? LastModifiedAt { get; set; }
  public string? CreatedBy { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Domain.Entities.Job, JobDto>()
        .ForMember(d => d.CustomerName, opt => opt.MapFrom(s => s.Customer.User.FullName.GetFullName()))
        .ForMember(d => d.CustomerEmail, opt => opt.MapFrom(s => s.Customer.User.Email.Value))
        .ForMember(d => d.CustomerPhone, opt => opt.MapFrom(s => s.Customer.User.PhoneNumber.ToString()))
        .ForMember(d => d.CustomerCompanyName, opt => opt.MapFrom(s => s.Customer.CompanyName))
        .ForMember(d => d.ServiceStreet, opt => opt.MapFrom(s => s.ServiceAddress.Street))
        .ForMember(d => d.ServiceCity, opt => opt.MapFrom(s => s.ServiceAddress.City))
        .ForMember(d => d.ServiceState, opt => opt.MapFrom(s => s.ServiceAddress.State))
        .ForMember(d => d.ServicePostalCode, opt => opt.MapFrom(s => s.ServiceAddress.PostalCode))
        .ForMember(d => d.ServiceCountry, opt => opt.MapFrom(s => s.ServiceAddress.Country))
        .ForMember(d => d.ServiceLatitude, opt => opt.MapFrom(s => s.ServiceAddress.Coordinates != null ? s.ServiceAddress.Coordinates.Latitude : (decimal?)null))
        .ForMember(d => d.ServiceLongitude, opt => opt.MapFrom(s => s.ServiceAddress.Coordinates != null ? s.ServiceAddress.Coordinates.Longitude : (decimal?)null))
        .ForMember(d => d.EstimatedCost, opt => opt.MapFrom(s => s.EstimatedCost != null ? s.EstimatedCost.Amount : (decimal?)null))
        .ForMember(d => d.ActualCost, opt => opt.MapFrom(s => s.ActualCost != null ? s.ActualCost.Amount : (decimal?)null))
        .ForMember(d => d.Currency, opt => opt.MapFrom(s => s.EstimatedCost != null ? s.EstimatedCost.Currency.ToString() : "TRY"))
        .ForMember(d => d.ActualDurationMinutes, opt => opt.MapFrom(s => s.ActualStartTime.HasValue && s.ActualEndTime.HasValue ? (int?)(s.ActualEndTime.Value - s.ActualStartTime.Value).TotalMinutes : null))
        .ForMember(d => d.PrimaryTechnicianId, opt => opt.MapFrom(s => s.JobAssignments.FirstOrDefault(ja => ja.IsPrimary) != null ? s.JobAssignments.FirstOrDefault(ja => ja.IsPrimary)!.TechnicianId : (Guid?)null))
        .ForMember(d => d.PrimaryTechnicianName, opt => opt.MapFrom(s => s.JobAssignments.FirstOrDefault(ja => ja.IsPrimary) != null ? s.JobAssignments.FirstOrDefault(ja => ja.IsPrimary)!.Technician.User.FullName.GetFullName() : null))
        .ForMember(d => d.TotalMaterialCost, opt => opt.MapFrom(s => s.JobMaterials.Sum(jm => jm.TotalCost != null ? jm.TotalCost.Amount : 0)))
        .ForMember(d => d.HasQuote, opt => opt.MapFrom(s => s.Quote != null))
        .ForMember(d => d.QuoteId, opt => opt.MapFrom(s => s.Quote != null ? s.Quote.Id : (Guid?)null))
        .ForMember(d => d.HasInvoice, opt => opt.MapFrom(s => s.Invoice != null))
        .ForMember(d => d.InvoiceId, opt => opt.MapFrom(s => s.Invoice != null ? s.Invoice.Id : (Guid?)null))
        .ForMember(d => d.HasFeedback, opt => opt.MapFrom(s => s.Feedback != null))
        .ForMember(d => d.FeedbackId, opt => opt.MapFrom(s => s.Feedback != null ? s.Feedback.Id : (Guid?)null))
        .ForMember(d => d.PhotoUrls, opt => opt.MapFrom(s => (s.PhotoUrls == null || s.PhotoUrls.Count == 0) ? new List<string>() : Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(JsonHelper.ToJsonArray(s.PhotoUrls))))
        .ForMember(d => d.Attachments, opt => opt.MapFrom(s => (s.Attachments == null || s.Attachments.Count == 0) ? new List<string>() : Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(JsonHelper.ToJsonArray(s.Attachments))))
        .ForMember(d => d.RecurrencePattern, opt => opt.MapFrom(s => string.IsNullOrEmpty(s.RecurrencePattern) ? null : Newtonsoft.Json.JsonConvert.DeserializeObject<RecurrencePatternDto>(s.RecurrencePattern)));
  }
}

