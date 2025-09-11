using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Notification;
using TakeServUs.Application.Interfaces.Services;
using TakeServUs.Domain.Enums;

namespace TakeServUs.Application.Services;

public class NotificationService : INotificationService
{
  public Task<ServiceResult<NotificationDto>> CreateAsync(NotificationCreateDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<NotificationDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<int>> GetUnreadCountAsync(Guid userId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<PagedResponse<NotificationDto>>> GetUserNotificationsAsync(Guid userId, PagedRequest request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> MarkAllAsReadAsync(Guid userId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> MarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> SendInvoiceNotificationAsync(Guid invoiceId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> SendJobAssignmentNotificationAsync(Guid jobId, Guid technicianId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> SendJobStatusNotificationAsync(Guid jobId, JobStatusType newStatus, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> SendPaymentReceivedNotificationAsync(Guid invoiceId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}
