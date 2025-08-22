using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.Features.Feedbacks.Commands;

public class SubmitFeedbackCommand : IRequest<FeedbackDto>
{
  public Guid JobId { get; set; }
  public Guid CustomerId { get; set; }
  public FeedbackRating OverallRating { get; set; }
  public string? Comments { get; set; }
}

public class SubmitFeedbackCommandHandler : IRequestHandler<SubmitFeedbackCommand, FeedbackDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public SubmitFeedbackCommandHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<FeedbackDto> Handle(SubmitFeedbackCommand request, CancellationToken cancellationToken)
  {
    // Business Rule: Check if feedback for this job already exists.
    var existingFeedback = await _context.Feedbacks
        .FirstOrDefaultAsync(f => f.JobId == request.JobId, cancellationToken);

    if (existingFeedback != null)
    {
      throw new InvalidOperationException("Feedback has already been submitted for this job.");
    }

    var feedback = new Feedback
    {
      JobId = request.JobId,
      CustomerId = request.CustomerId,
      OverallRating = request.OverallRating,
      Comments = request.Comments,
      IsPublic = true
    };

    _context.Feedbacks.Add(feedback);
    await _context.SaveChangesAsync(cancellationToken);

    return _mapper.Map<FeedbackDto>(feedback);
  }
}

public class SubmitFeedbackCommandValidator : AbstractValidator<SubmitFeedbackCommand>
{
  public SubmitFeedbackCommandValidator()
  {
    RuleFor(x => x.JobId).NotEmpty();
    RuleFor(x => x.CustomerId).NotEmpty();
    RuleFor(x => x.OverallRating).IsInEnum();
  }
}
