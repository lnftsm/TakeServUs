using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Notification;
using TakeServUs.Domain.Enums;

namespace TakeServUs.Application.Interfaces.Services;

public interface INotificationService
{
  Task<ServiceResult<NotificationDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<ServiceResult<PagedResponse<NotificationDto>>> GetUserNotificationsAsync(Guid userId, PagedRequest request, CancellationToken cancellationToken = default);
  Task<ServiceResult<NotificationDto>> CreateAsync(NotificationCreateDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> MarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> MarkAllAsReadAsync(Guid userId, CancellationToken cancellationToken = default);
  Task<ServiceResult<int>> GetUnreadCountAsync(Guid userId, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

  // Notification sending
  Task<ServiceResult<bool>> SendJobAssignmentNotificationAsync(Guid jobId, Guid technicianId, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> SendJobStatusNotificationAsync(Guid jobId, JobStatusType newStatus, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> SendInvoiceNotificationAsync(Guid invoiceId, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> SendPaymentReceivedNotificationAsync(Guid invoiceId, CancellationToken cancellationToken = default);
}
