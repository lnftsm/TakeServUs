using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;

namespace TakeServUs.Application.Features.Feedbacks.Queries;

public class GetFeedbackByJobIdQuery : IRequest<FeedbackDto>
{
  public Guid JobId { get; set; }
}

public class GetFeedbackByJobIdQueryHandler : IRequestHandler<GetFeedbackByJobIdQuery, FeedbackDto?>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public GetFeedbackByJobIdQueryHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<FeedbackDto?> Handle(GetFeedbackByJobIdQuery request, CancellationToken cancellationToken)
  {
    var feedback = await _context.Feedbacks
        .AsNoTracking()
        .Include(f => f.Customer).ThenInclude(c => c.User)
        .FirstOrDefaultAsync(f => f.JobId == request.JobId, cancellationToken);

    if (feedback == null)
    {
      // Returning null is often better than throwing an exception for this query
      return null;
    }

    return _mapper.Map<FeedbackDto>(feedback);
  }
}
