using MediatR;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.Features.Invoices.Commands;

public class CancelInvoiceCommand : IRequest<Unit>
{
  public Guid InvoiceId { get; set; }
}

public class CancelInvoiceCommandHandler : IRequestHandler<CancelInvoiceCommand, Unit>
{
  private readonly IApplicationDbContext _context;

  public CancelInvoiceCommandHandler(IApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<Unit> Handle(CancelInvoiceCommand request, CancellationToken cancellationToken)
  {
    var invoice = await _context.Invoices.FindAsync(new object[] { request.InvoiceId }, cancellationToken);
    if (invoice == null)
    {
      throw new NotFoundException(nameof(Invoice), request.InvoiceId);
    }

    // Business rule: You can only cancel unpaid invoices.
    if (invoice.PaymentStatus == PaymentStatus.Paid || invoice.PaymentStatus == PaymentStatus.PartiallyPaid)
    {
      throw new InvalidOperationException("Cannot cancel an invoice that has payments recorded.");
    }

    invoice.PaymentStatus = PaymentStatus.Cancelled;
    invoice.IsDeleted = true; // Soft delete

    await _context.SaveChangesAsync(cancellationToken);
    return Unit.Value;
  }
}
