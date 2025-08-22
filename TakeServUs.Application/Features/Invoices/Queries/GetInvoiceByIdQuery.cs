using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.Features.Invoices.Queries;

public class GetInvoiceByIdQuery : IRequest<InvoiceDto>
{
  public Guid Id { get; set; }
}

public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, InvoiceDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public GetInvoiceByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<InvoiceDto> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
  {
    var invoice = await _context.Invoices
        .AsNoTracking()
        .Include(i => i.Items)
        .Include(i => i.Customer).ThenInclude(c => c.User)
        .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

    if (invoice == null)
    {
      throw new NotFoundException(nameof(Invoice), request.Id);
    }

    return _mapper.Map<InvoiceDto>(invoice);
  }
}
