using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Email;
using TakeServUs.Application.Interfaces.Services;

namespace TakeServUs.Application.Services;

public class QueuedEmailService : IQueuedEmailService
{
  public Task<ServiceResult<bool>> CancelQueuedEmailAsync(Guid emailId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<int>> CleanupOldEmailsAsync(int daysToKeep = 30, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<EmailStatisticsDto>> GetEmailStatisticsAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<QueuedEmailDto>>> GetFailedEmailsAsync(DateTime since, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<QueuedEmailDto>>> GetPendingEmailsAsync(int count = 50, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<EmailQueueStatusDto>> GetQueueStatusAsync(CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<int>> ProcessQueuedEmailsAsync(int batchSize = 10, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> QueueBulkEmailsAsync(IEnumerable<EmailQueueDto> emails, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<Guid>> QueueEmailAsync(EmailQueueDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<Guid>> QueueEmailConfirmationAsync(string toEmail, string confirmationToken, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<Guid>> QueueFeedbackRequestEmailAsync(Guid jobId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<Guid>> QueueInvoiceEmailAsync(Guid invoiceId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<Guid>> QueueJobAssignmentEmailAsync(Guid jobId, Guid technicianId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<Guid>> QueueJobCompletionEmailAsync(Guid jobId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<Guid>> QueuePasswordResetEmailAsync(string toEmail, string resetToken, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<Guid>> QueuePaymentReceivedEmailAsync(Guid invoiceId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<Guid>> QueueWelcomeEmailAsync(string toEmail, string userName, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> RetryFailedEmailAsync(Guid emailId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}
