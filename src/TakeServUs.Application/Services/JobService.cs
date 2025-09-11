using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Job;
using TakeServUs.Application.Interfaces.Services;
using TakeServUs.Domain.Enums;

namespace TakeServUs.Application.Services;

public class JobService : IJobService
{
  public Task<ServiceResult<bool>> AddCustomerSignatureAsync(Guid jobId, string signatureData, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> AddMaterialToJobAsync(Guid jobId, Guid materialId, decimal quantity, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<JobDto>> AssignTechnicianAsync(Guid jobId, Guid technicianId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<JobDto>> CreateAsync(JobCreateDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<JobDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<JobListDto>>> GetJobsByCustomerAsync(Guid customerId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<JobListDto>>> GetJobsByStatusAsync(JobStatusType status, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<JobListDto>>> GetJobsByTechnicianAsync(Guid technicianId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<PagedResponse<JobListDto>>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<JobListDto>>> GetTodaysJobsAsync(CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> UnassignTechnicianAsync(Guid jobId, Guid technicianId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<JobDto>> UpdateAsync(Guid id, JobUpdateDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<JobDto>> UpdateStatusAsync(Guid jobId, JobStatusUpdateDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> UploadJobPhotoAsync(Guid jobId, Stream fileStream, string fileName, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}
