using MediatR;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;

namespace TakeServUs.Application.Features.Notifications.Commands;

public class MarkNotificationAsReadCommand : IRequest<Unit>
{
  public Guid NotificationId { get; set; }
}

public class MarkNotificationAsReadCommandHandler : IRequestHandler<MarkNotificationAsReadCommand, Unit>
{
  private readonly IApplicationDbContext _context;
  private readonly ICurrentUserService _currentUserService;

  public MarkNotificationAsReadCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
  {
    _context = context;
    _currentUserService = currentUserService;
  }

  public async Task<Unit> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
  {
    var notification = await _context.Notifications.FindAsync(new object[] { request.NotificationId }, cancellationToken);

    // Security check: Ensure the user is marking their own notification.
    if (notification == null || notification.UserId.ToString() != _currentUserService.UserId)
    {
      throw new NotFoundException(nameof(Domain.Entities.Notification), request.NotificationId);
    }

    if (!notification.IsRead)
    {
      notification.IsRead = true;
      notification.ReadAt = DateTime.UtcNow;
      await _context.SaveChangesAsync(cancellationToken);
    }

    return Unit.Value;
  }
}
