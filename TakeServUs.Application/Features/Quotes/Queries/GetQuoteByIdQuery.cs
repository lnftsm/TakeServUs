using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.Features.Quotes.Queries;

public class GetQuoteByIdQuery : IRequest<QuoteDto>
{
  public Guid Id { get; set; }
}

public class GetQuoteByIdQueryHandler : IRequestHandler<GetQuoteByIdQuery, QuoteDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public GetQuoteByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<QuoteDto> Handle(GetQuoteByIdQuery request, CancellationToken cancellationToken)
  {
    var quote = await _context.Quotes
        .AsNoTracking()
        .Include(q => q.Items)
        .FirstOrDefaultAsync(q => q.Id == request.Id, cancellationToken);

    if (quote == null)
    {
      throw new NotFoundException(nameof(Quote), request.Id);
    }

    return _mapper.Map<QuoteDto>(quote);
  }
}
