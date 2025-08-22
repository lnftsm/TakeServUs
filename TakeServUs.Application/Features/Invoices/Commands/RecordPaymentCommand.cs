using AutoMapper;
using MediatR;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.Entities;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Application.Features.Invoices.Commands;

public class RecordPaymentCommand : IRequest<InvoiceDto>
{
  public Guid InvoiceId { get; set; }
  public decimal Amount { get; set; }
  public DateTime PaymentDate { get; set; }
  public PaymentMethod PaymentMethod { get; set; }
  public string? PaymentReference { get; set; }
}

public class RecordPaymentCommandHandler : IRequestHandler<RecordPaymentCommand, InvoiceDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public RecordPaymentCommandHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<InvoiceDto> Handle(RecordPaymentCommand request, CancellationToken cancellationToken)
  {
    var invoice = await _context.Invoices.FindAsync(new object[] { request.InvoiceId }, cancellationToken);
    if (invoice == null)
    {
      throw new NotFoundException(nameof(Invoice), request.InvoiceId);
    }

    var paymentAmount = new Money(request.Amount, invoice.TotalAmount.Currency);
    invoice.PaidAmount = invoice.PaidAmount.Add(paymentAmount);
    invoice.PaidAt = request.PaymentDate;
    invoice.PaymentMethod = request.PaymentMethod;
    invoice.PaymentReference = request.PaymentReference;

    if (invoice.PaidAmount.Amount >= invoice.TotalAmount.Amount)
    {
      invoice.PaymentStatus = PaymentStatus.Paid;
    }
    else if (invoice.PaidAmount.Amount > 0)
    {
      invoice.PaymentStatus = PaymentStatus.PartiallyPaid;
    }

    await _context.SaveChangesAsync(cancellationToken);
    return _mapper.Map<InvoiceDto>(invoice);
  }
}
