using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Email;

namespace TakeServUs.Application.Interfaces.Services;

public interface IQueuedEmailService
{
  // Email queue management
  Task<ServiceResult<Guid>> QueueEmailAsync(EmailQueueDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> QueueBulkEmailsAsync(IEnumerable<EmailQueueDto> emails, CancellationToken cancellationToken = default);
  Task<ServiceResult<int>> ProcessQueuedEmailsAsync(int batchSize = 10, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> RetryFailedEmailAsync(Guid emailId, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> CancelQueuedEmailAsync(Guid emailId, CancellationToken cancellationToken = default);

  // Email templates
  Task<ServiceResult<Guid>> QueueWelcomeEmailAsync(string toEmail, string userName, CancellationToken cancellationToken = default);
  Task<ServiceResult<Guid>> QueuePasswordResetEmailAsync(string toEmail, string resetToken, CancellationToken cancellationToken = default);
  Task<ServiceResult<Guid>> QueueEmailConfirmationAsync(string toEmail, string confirmationToken, CancellationToken cancellationToken = default);
  Task<ServiceResult<Guid>> QueueJobAssignmentEmailAsync(Guid jobId, Guid technicianId, CancellationToken cancellationToken = default);
  Task<ServiceResult<Guid>> QueueJobCompletionEmailAsync(Guid jobId, CancellationToken cancellationToken = default);
  Task<ServiceResult<Guid>> QueueInvoiceEmailAsync(Guid invoiceId, CancellationToken cancellationToken = default);
  Task<ServiceResult<Guid>> QueuePaymentReceivedEmailAsync(Guid invoiceId, CancellationToken cancellationToken = default);
  Task<ServiceResult<Guid>> QueueFeedbackRequestEmailAsync(Guid jobId, CancellationToken cancellationToken = default);

  // Monitoring and reporting
  Task<ServiceResult<EmailQueueStatusDto>> GetQueueStatusAsync(CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<QueuedEmailDto>>> GetPendingEmailsAsync(int count = 50, CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<QueuedEmailDto>>> GetFailedEmailsAsync(DateTime since, CancellationToken cancellationToken = default);
  Task<ServiceResult<EmailStatisticsDto>> GetEmailStatisticsAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);
  Task<ServiceResult<int>> CleanupOldEmailsAsync(int daysToKeep = 30, CancellationToken cancellationToken = default);
}
