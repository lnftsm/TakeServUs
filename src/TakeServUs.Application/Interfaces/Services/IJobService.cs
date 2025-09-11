using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Job;
using TakeServUs.Domain.Enums;

namespace TakeServUs.Application.Interfaces.Services;

public interface IJobService
{
  Task<ServiceResult<JobDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<ServiceResult<PagedResponse<JobListDto>>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default);
  Task<ServiceResult<JobDto>> CreateAsync(JobCreateDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<JobDto>> UpdateAsync(Guid id, JobUpdateDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

  // Job specific operations
  Task<ServiceResult<JobDto>> AssignTechnicianAsync(Guid jobId, Guid technicianId, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> UnassignTechnicianAsync(Guid jobId, Guid technicianId, CancellationToken cancellationToken = default);
  Task<ServiceResult<JobDto>> UpdateStatusAsync(Guid jobId, JobStatusUpdateDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<JobListDto>>> GetJobsByTechnicianAsync(Guid technicianId, CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<JobListDto>>> GetJobsByCustomerAsync(Guid customerId, CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<JobListDto>>> GetJobsByStatusAsync(JobStatusType status, CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<JobListDto>>> GetTodaysJobsAsync(CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> AddMaterialToJobAsync(Guid jobId, Guid materialId, decimal quantity, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> UploadJobPhotoAsync(Guid jobId, Stream fileStream, string fileName, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> AddCustomerSignatureAsync(Guid jobId, string signatureData, CancellationToken cancellationToken = default);
}
