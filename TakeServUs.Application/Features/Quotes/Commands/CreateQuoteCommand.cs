using AutoMapper;
using MediatR;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.Entities;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Application.Features.Quotes.Commands;

public class CreateQuoteCommand : IRequest<QuoteDto>
{
  public Guid JobId { get; set; }
  public DateTime ValidUntil { get; set; }
  public List<CreateQuoteItemDto> Items { get; set; } = new();
  public decimal? TaxRate { get; set; }
  public decimal? DiscountAmount { get; set; }
  public string? Terms { get; set; }
}

public class CreateQuoteItemDto
{
  public required string Description { get; set; }
  public required decimal Quantity { get; set; }
  public required decimal UnitPrice { get; set; }
}

public class CreateQuoteCommandHandler : IRequestHandler<CreateQuoteCommand, QuoteDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public CreateQuoteCommandHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<QuoteDto> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
  {
    var currency = Currency.TRY; // Assume default currency

    var quoteItems = request.Items.Select(item => new QuoteItem
    {
      Description = item.Description,
      Quantity = item.Quantity,
      UnitPrice = new Money(item.UnitPrice, currency),
      TotalPrice = new Money(item.Quantity * item.UnitPrice, currency)
    }).ToList();

    var subTotal = new Money(quoteItems.Sum(i => i.TotalPrice.Amount), currency);
    var taxAmount = request.TaxRate.HasValue ? subTotal.Multiply(request.TaxRate.Value / 100) : new Money(0, currency);
    var discount = new Money(request.DiscountAmount ?? 0, currency);
    var totalAmount = subTotal.Add(taxAmount).Subtract(discount);

    var quote = new Quote
    {
      JobId = request.JobId,
      QuoteNumber = $"QUO-{DateTime.UtcNow.Ticks}",
      QuoteDate = DateTime.UtcNow,
      ValidUntil = request.ValidUntil,
      Items = quoteItems,
      SubTotal = subTotal,
      TaxAmount = taxAmount,
      DiscountAmount = discount,
      TotalAmount = totalAmount,
      Terms = request.Terms,
      IsAccepted = false
    };

    _context.Quotes.Add(quote);
    await _context.SaveChangesAsync(cancellationToken);

    return _mapper.Map<QuoteDto>(quote);
  }
}
