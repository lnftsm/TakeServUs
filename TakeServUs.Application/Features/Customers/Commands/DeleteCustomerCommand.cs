using MediatR;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;

namespace TakeServUs.Application.Features.Customers.Commands;

public class DeleteCustomerCommand : IRequest<Unit>
{
  public Guid Id { get; set; }
}

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
{
  private readonly IApplicationDbContext _context;

  public DeleteCustomerCommandHandler(IApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
  {
    var customer = await _context.Customers.FindAsync(new object[] { request.Id }, cancellationToken);

    if (customer == null)
    {
      throw new NotFoundException(nameof(Domain.Entities.Customer), request.Id);
    }

    var user = await _context.Users.FindAsync(new object[] { customer.UserId }, cancellationToken);
    if (user != null)
    {
      user.IsDeleted = true; // Soft-delete the user
    }

    customer.IsDeleted = true; // Soft-delete the customer

    await _context.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }
}
