using AutoMapper;
using MediatR;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.Features.Feedbacks.Commands;

public class RespondToFeedbackCommand : IRequest<FeedbackDto>
{
  public Guid FeedbackId { get; set; }
  public required string Response { get; set; }
}

public class RespondToFeedbackCommandHandler : IRequestHandler<RespondToFeedbackCommand, FeedbackDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;
  private readonly ICurrentUserService _currentUserService;

  public RespondToFeedbackCommandHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
  {
    _context = context;
    _mapper = mapper;
    _currentUserService = currentUserService;
  }

  public async Task<FeedbackDto> Handle(RespondToFeedbackCommand request, CancellationToken cancellationToken)
  {
    var feedback = await _context.Feedbacks.FindAsync(new object[] { request.FeedbackId }, cancellationToken);
    if (feedback == null)
    {
      throw new NotFoundException(nameof(Feedback), request.FeedbackId);
    }

    feedback.Response = request.Response;
    feedback.RespondedAt = DateTime.UtcNow;
    feedback.RespondedBy = _currentUserService.UserId;

    await _context.SaveChangesAsync(cancellationToken);

    return _mapper.Map<FeedbackDto>(feedback);
  }
}
