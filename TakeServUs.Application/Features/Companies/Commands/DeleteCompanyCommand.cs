using MediatR;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;


namespace TakeServUs.Application.Features.Companies.Commands;

public class DeleteCompanyCommand : IRequest<Unit> // Returns nothing
{
  public Guid Id { get; set; }
}

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, Unit>
{
  private readonly IApplicationDbContext _context;
  private readonly ICurrentUserService _currentUserService; // To track who deleted it

  public DeleteCompanyCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
  {
    _context = context;
    _currentUserService = currentUserService;
  }

  public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
  {
    var company = await _context.Companies.FindAsync(new object[] { request.Id }, cancellationToken);

    if (company == null)
    {
      throw new NotFoundException(nameof(Domain.Entities.Company), request.Id);
    }

    // Soft delete logic from the domain entity
    company.IsDeleted = true;
    company.DeletedAt = DateTime.UtcNow;
    company.DeletedBy = _currentUserService.UserId;

    // In a real scenario, you might also want to handle related entities
    // (e.g., deactivate users, cancel jobs).

    await _context.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
