using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Feedback;
using TakeServUs.Application.Interfaces.Services;

namespace TakeServUs.Application.Services;

public class FeedbackService : IFeedbackService
{
  public Task<ServiceResult<FeedbackDto>> CreateAsync(FeedbackCreateDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<double>> GetAverageRatingForCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<double>> GetAverageRatingForTechnicianAsync(Guid technicianId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<FeedbackListDto>>> GetByCustomerAsync(Guid customerId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<FeedbackDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<FeedbackDto>> GetByJobIdAsync(Guid jobId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<FeedbackListDto>>> GetByTechnicianAsync(Guid technicianId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<PagedResponse<FeedbackListDto>>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<FeedbackListDto>>> GetPublicFeedbacksAsync(int count = 10, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<FeedbackStatisticsDto>> GetStatisticsAsync(Guid? companyId = null, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> RequestFeedbackAsync(Guid jobId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<FeedbackDto>> RespondToFeedbackAsync(Guid feedbackId, string response, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> TogglePublicVisibilityAsync(Guid feedbackId, bool isPublic, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}
