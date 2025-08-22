using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;

namespace TakeServUs.Application.Features.Quotes.Queries;

public class GetQuotesByJobIdQuery : IRequest<List<QuoteDto>>
{
  public Guid JobId { get; set; }
}

public class GetQuotesByJobIdQueryHandler : IRequestHandler<GetQuotesByJobIdQuery, List<QuoteDto>>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public GetQuotesByJobIdQueryHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<List<QuoteDto>> Handle(GetQuotesByJobIdQuery request, CancellationToken cancellationToken)
  {
    var quotes = await _context.Quotes
        .AsNoTracking()
        .Where(q => q.JobId == request.JobId)
        .OrderByDescending(q => q.QuoteDate)
        .ToListAsync(cancellationToken);

    return _mapper.Map<List<QuoteDto>>(quotes);
  }
}
