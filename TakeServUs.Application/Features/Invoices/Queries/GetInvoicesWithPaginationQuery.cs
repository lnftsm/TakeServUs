using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.Features.Invoices.Queries;

public class GetInvoicesWithPaginationQuery : IRequest<PaginatedList<InvoiceDto>>
{
  public Guid CompanyId { get; set; } // Invoices are linked via Customer/Job to a Company
  public Guid? CustomerIdFilter { get; set; }
  public PaymentStatus? StatusFilter { get; set; }
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 10;
}

public class GetInvoicesWithPaginationQueryHandler : IRequestHandler<GetInvoicesWithPaginationQuery, PaginatedList<InvoiceDto>>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public GetInvoicesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<PaginatedList<InvoiceDto>> Handle(GetInvoicesWithPaginationQuery request, CancellationToken cancellationToken)
  {
    var query = _context.Invoices
        .AsNoTracking()
        .Include(i => i.Customer)
        .Where(i => i.Customer.CompanyId == request.CompanyId);

    if (request.CustomerIdFilter.HasValue)
    {
      query = query.Where(i => i.CustomerId == request.CustomerIdFilter.Value);
    }

    if (request.StatusFilter.HasValue)
    {
      query = query.Where(i => i.PaymentStatus == request.StatusFilter.Value);
    }

    var paginatedInvoices = await PaginatedList<Invoice>.CreateAsync(query, request.PageNumber, request.PageSize);

    var invoiceDtos = _mapper.Map<List<InvoiceDto>>(paginatedInvoices.Items);

    return new PaginatedList<InvoiceDto>(invoiceDtos, paginatedInvoices.TotalCount, paginatedInvoices.PageNumber, request.PageSize);
  }
}
