using MediatR;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;

namespace TakeServUs.Application.Features.Materials.Commands;

public class DeleteMaterialCommand : IRequest<Unit>
{
  public Guid Id { get; set; }
}

public class DeleteMaterialCommandHandler : IRequestHandler<DeleteMaterialCommand, Unit>
{
  private readonly IApplicationDbContext _context;

  public DeleteMaterialCommandHandler(IApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<Unit> Handle(DeleteMaterialCommand request, CancellationToken cancellationToken)
  {
    var material = await _context.Materials.FindAsync(new object[] { request.Id }, cancellationToken);

    if (material == null)
    {
      throw new NotFoundException(nameof(Domain.Entities.Material), request.Id);
    }

    material.IsDeleted = true; // Soft delete

    await _context.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
