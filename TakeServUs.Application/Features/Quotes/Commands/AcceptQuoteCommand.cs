using AutoMapper;
using MediatR;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.Features.Quotes.Commands;

public class AcceptQuoteCommand : IRequest<QuoteDto>
{
  public Guid QuoteId { get; set; }
}

public class AcceptQuoteCommandHandler : IRequestHandler<AcceptQuoteCommand, QuoteDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;
  private readonly ICurrentUserService _currentUserService;

  public AcceptQuoteCommandHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
  {
    _context = context;
    _mapper = mapper;
    _currentUserService = currentUserService;
  }

  public async Task<QuoteDto> Handle(AcceptQuoteCommand request, CancellationToken cancellationToken)
  {
    var quote = await _context.Quotes.FindAsync(new object[] { request.QuoteId }, cancellationToken);
    if (quote == null)
    {
      throw new NotFoundException(nameof(Quote), request.QuoteId);
    }

    if (quote.IsAccepted)
    {
      throw new InvalidOperationException("This quote has already been accepted.");
    }

    quote.IsAccepted = true;
    quote.AcceptedAt = DateTime.UtcNow;
    quote.AcceptedBy = _currentUserService.UserId;

    // Business Logic: When a quote is accepted, you might want to update the related Job status.
    var job = await _context.Jobs.FindAsync(new object[] { quote.JobId }, cancellationToken);
    if (job != null)
    {
      job.CurrentStatus = Domain.Entities.JobStatusType.Accepted;
      job.EstimatedCost = quote.TotalAmount;
    }

    await _context.SaveChangesAsync(cancellationToken);
    return _mapper.Map<QuoteDto>(quote);
  }
}
