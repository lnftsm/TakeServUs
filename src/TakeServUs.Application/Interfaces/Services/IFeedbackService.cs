using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Feedback;
using TakeServUs.Domain.Enums;

namespace TakeServUs.Application.Interfaces.Services;

public interface IFeedbackService
{
  Task<ServiceResult<FeedbackDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<ServiceResult<PagedResponse<FeedbackListDto>>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default);
  Task<ServiceResult<FeedbackDto>> CreateAsync(FeedbackCreateDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

  // Feedback specific operations
  Task<ServiceResult<FeedbackDto>> GetByJobIdAsync(Guid jobId, CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<FeedbackListDto>>> GetByCustomerAsync(Guid customerId, CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<FeedbackListDto>>> GetByTechnicianAsync(Guid technicianId, CancellationToken cancellationToken = default);
  Task<ServiceResult<FeedbackDto>> RespondToFeedbackAsync(Guid feedbackId, string response, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> TogglePublicVisibilityAsync(Guid feedbackId, bool isPublic, CancellationToken cancellationToken = default);
  Task<ServiceResult<double>> GetAverageRatingForTechnicianAsync(Guid technicianId, CancellationToken cancellationToken = default);
  Task<ServiceResult<double>> GetAverageRatingForCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);
  Task<ServiceResult<FeedbackStatisticsDto>> GetStatisticsAsync(Guid? companyId = null, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<FeedbackListDto>>> GetPublicFeedbacksAsync(int count = 10, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> RequestFeedbackAsync(Guid jobId, CancellationToken cancellationToken = default);
}
