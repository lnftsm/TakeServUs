using AutoMapper;
using MediatR;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.Entities;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Application.Features.Invoices.Commands;

public class CreateInvoiceCommand : IRequest<InvoiceDto>
{
  public Guid JobId { get; set; }
  public Guid CustomerId { get; set; }
  public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
  public DateTime DueDate { get; set; }
  public List<CreateInvoiceItemDto> Items { get; set; } = new();
  public decimal? TaxRate { get; set; } // e.g., 18 for 18%
  public decimal? DiscountAmount { get; set; }
}

public class CreateInvoiceItemDto
{
  public required string Description { get; set; }
  public required decimal Quantity { get; set; }
  public required decimal UnitPrice { get; set; }
}


public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, InvoiceDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public CreateInvoiceCommandHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<InvoiceDto> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
  {
    // In a real app, you would get the currency from the company or customer settings.
    var currency = Currency.TRY;

    var invoiceItems = request.Items.Select(item => new InvoiceItem
    {
      Description = item.Description,
      Quantity = item.Quantity,
      UnitPrice = new Money(item.UnitPrice, currency),
      TotalPrice = new Money(item.Quantity * item.UnitPrice, currency)
    }).ToList();

    var subTotal = new Money(invoiceItems.Sum(i => i.TotalPrice.Amount), currency);
    var taxAmount = request.TaxRate.HasValue ? subTotal.Multiply(request.TaxRate.Value / 100) : new Money(0, currency);
    var discount = new Money(request.DiscountAmount ?? 0, currency);
    var totalAmount = subTotal.Add(taxAmount).Subtract(discount);

    var invoice = new Invoice
    {
      JobId = request.JobId,
      CustomerId = request.CustomerId,
      InvoiceNumber = $"INV-{DateTime.UtcNow.Ticks}", // Use a better generation service
      InvoiceDate = request.InvoiceDate,
      DueDate = request.DueDate,
      Items = invoiceItems,
      SubTotal = subTotal,
      TaxAmount = taxAmount,
      DiscountAmount = discount,
      TotalAmount = totalAmount,
      PaidAmount = new Money(0, currency),
      PaymentStatus = PaymentStatus.Pending
    };

    _context.Invoices.Add(invoice);
    await _context.SaveChangesAsync(cancellationToken);

    return _mapper.Map<InvoiceDto>(invoice);
  }
}
