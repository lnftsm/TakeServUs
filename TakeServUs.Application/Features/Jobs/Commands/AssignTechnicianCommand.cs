using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.Features.Jobs.Commands;

public class AssignTechnicianCommand : IRequest<Unit>
{
  public Guid JobId { get; set; }
  public Guid TechnicianId { get; set; }
  public bool IsPrimary { get; set; } = true;
}

public class AssignTechnicianCommandHandler : IRequestHandler<AssignTechnicianCommand, Unit>
{
  private readonly IApplicationDbContext _context;

  public AssignTechnicianCommandHandler(IApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<Unit> Handle(AssignTechnicianCommand request, CancellationToken cancellationToken)
  {
    var job = await _context.Jobs.FindAsync(new object[] { request.JobId }, cancellationToken);
    if (job == null)
    {
      throw new NotFoundException(nameof(Job), request.JobId);
    }

    var technician = await _context.Technicians.FindAsync(new object[] { request.TechnicianId }, cancellationToken);
    if (technician == null)
    {
      throw new NotFoundException(nameof(Technician), request.TechnicianId);
    }

    // Business Rule: Check if the technician is already assigned to this job.
    var existingAssignment = await _context.JobAssignments
        .FirstOrDefaultAsync(ja => ja.JobId == request.JobId && ja.TechnicianId == request.TechnicianId, cancellationToken);

    if (existingAssignment != null)
    {
      // Or just return silently if this is not an error.
      throw new InvalidOperationException("This technician is already assigned to this job.");
    }

    var assignment = new JobAssignment
    {
      JobId = request.JobId,
      TechnicianId = request.TechnicianId,
      AssignedAt = DateTime.UtcNow,
      IsPrimary = request.IsPrimary
    };

    _context.JobAssignments.Add(assignment);

    // Update the job status to "Assigned"
    job.CurrentStatus = JobStatusType.Assigned;

    await _context.SaveChangesAsync(cancellationToken);

    // TODO: Trigger a notification to the technician.

    return Unit.Value;
  }
}
