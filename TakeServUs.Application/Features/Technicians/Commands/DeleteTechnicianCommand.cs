using MediatR;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;

namespace TakeServUs.Application.Features.Technicians.Commands;

public class DeleteTechnicianCommand : IRequest<Unit>
{
  public Guid Id { get; set; }
}

public class DeleteTechnicianCommandHandler : IRequestHandler<DeleteTechnicianCommand, Unit>
{
  private readonly IApplicationDbContext _context;

  public DeleteTechnicianCommandHandler(IApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<Unit> Handle(DeleteTechnicianCommand request, CancellationToken cancellationToken)
  {
    var technician = await _context.Technicians.FindAsync(new object[] { request.Id }, cancellationToken);

    if (technician == null)
    {
      throw new NotFoundException(nameof(Domain.Entities.Technician), request.Id);
    }

    var user = await _context.Users.FindAsync(new object[] { technician.UserId }, cancellationToken);
    if (user != null)
    {
      user.IsDeleted = true; // Soft-delete the user
    }

    technician.IsDeleted = true; // Soft-delete the technician

    await _context.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
