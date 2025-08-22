using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs;

namespace TakeServUs.Application.Features.Notifications.Queries;

public class GetNotificationsForUserQuery : IRequest<PaginatedList<NotificationDto>>
{
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 15;
}

public class GetNotificationsForUserQueryHandler : IRequestHandler<GetNotificationsForUserQuery, PaginatedList<NotificationDto>>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;
  private readonly ICurrentUserService _currentUserService;

  public GetNotificationsForUserQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
  {
    _context = context;
    _mapper = mapper;
    _currentUserService = currentUserService;
  }

  public async Task<PaginatedList<NotificationDto>> Handle(GetNotificationsForUserQuery request, CancellationToken cancellationToken)
  {
    if (string.IsNullOrEmpty(_currentUserService.UserId))
    {
      throw new UnauthorizedAccessException();
    }

    var userId = Guid.Parse(_currentUserService.UserId);

    var query = _context.Notifications
        .AsNoTracking()
        .Where(n => n.UserId == userId)
        .OrderByDescending(n => n.CreatedAt);

    var paginatedNotifications = await PaginatedList<Domain.Entities.Notification>.CreateAsync(query, request.PageNumber, request.PageSize);

    var notificationDtos = _mapper.Map<List<NotificationDto>>(paginatedNotifications.Items);

    return new PaginatedList<NotificationDto>(notificationDtos, paginatedNotifications.TotalCount, paginatedNotifications.PageNumber, request.PageSize);
  }
}
