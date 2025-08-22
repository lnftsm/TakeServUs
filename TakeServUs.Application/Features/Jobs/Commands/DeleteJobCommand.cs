using MediatR;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.Features.Jobs.Commands;

public class DeleteJobCommand : IRequest<Unit>
{
  public Guid Id { get; set; }
}

public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, Unit>
{
  private readonly IApplicationDbContext _context;

  public DeleteJobCommandHandler(IApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<Unit> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
  {
    var job = await _context.Jobs.FindAsync(new object[] { request.Id }, cancellationToken);

    if (job == null)
    {
      throw new NotFoundException(nameof(Job), request.Id);
    }

    job.IsDeleted = true; // Soft delete

    await _context.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
