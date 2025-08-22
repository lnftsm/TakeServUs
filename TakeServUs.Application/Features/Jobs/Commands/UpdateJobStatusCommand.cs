using MediatR;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.Features.Jobs.Commands;

public class UpdateJobStatusCommand : IRequest<Unit>
{
  public Guid JobId { get; set; }
  public JobStatusType NewStatus { get; set; }
  public string? Notes { get; set; }
  public double? Latitude { get; set; }
  public double? Longitude { get; set; }
}

public class UpdateJobStatusCommandHandler : IRequestHandler<UpdateJobStatusCommand, Unit>
{
  private readonly IApplicationDbContext _context;
  private readonly ICurrentUserService _currentUserService;

  public UpdateJobStatusCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
  {
    _context = context;
    _currentUserService = currentUserService;
  }

  public async Task<Unit> Handle(UpdateJobStatusCommand request, CancellationToken cancellationToken)
  {
    var job = await _context.Jobs.FindAsync(new object[] { request.JobId }, cancellationToken);
    if (job == null)
    {
      throw new NotFoundException(nameof(Job), request.JobId);
    }

    // Update the current status on the job itself
    job.CurrentStatus = request.NewStatus;

    // Set actual start/end times based on status
    if (request.NewStatus == JobStatusType.Started && !job.ActualStartTime.HasValue)
    {
      job.ActualStartTime = DateTime.UtcNow;
    }
    else if (request.NewStatus == JobStatusType.Completed)
    {
      job.ActualEndTime = DateTime.UtcNow;
    }

    // Create a history record for this status change
    var statusHistory = new JobStatus
    {
      JobId = request.JobId,
      Status = request.NewStatus,
      ChangedAt = DateTime.UtcNow,
      ChangedBy = _currentUserService.UserId,
      Notes = request.Notes,
      Latitude = request.Latitude,
      Longitude = request.Longitude
    };

    // Add the new status history to the Job's collection.
    // EF Core will automatically detect this as a new related entity.
    job.StatusHistory.Add(statusHistory);

    await _context.SaveChangesAsync(cancellationToken);

    // TODO: Trigger notifications to the customer based on status (e.g., EnRoute, Completed).

    return Unit.Value;
  }
}
