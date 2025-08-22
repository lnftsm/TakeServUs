using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.DTOs;
using TakeServUs.Application.Common.Models;
using TakeServUs.Application.Common.Interfaces;

namespace TakeServUs.Application.Features.Customers.Queries;

public class GetCustomersWithPaginationQuery : IRequest<PaginatedList<CustomerDto>>
{
  public Guid CompanyId { get; set; } // Customers must belong to a company
  public string? NameFilter { get; set; }
  public string? EmailFilter { get; set; }
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 10;
}

public class GetCustomersWithPaginationQueryHandler : IRequestHandler<GetCustomersWithPaginationQuery, PaginatedList<CustomerDto>>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public GetCustomersWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<PaginatedList<CustomerDto>> Handle(GetCustomersWithPaginationQuery request, CancellationToken cancellationToken)
  {
    IQueryable<Domain.Entities.Customer> query = _context.Customers
    .Include(c => c.User)
    .AsNoTracking()
    .Where(c => c.CompanyId == request.CompanyId);

    if (!string.IsNullOrWhiteSpace(request.NameFilter))
    {
      query = query.Where(c => c.User.FullName.FirstName.Contains(request.NameFilter) || c.User.FullName.LastName.Contains(request.NameFilter));
    }

    if (!string.IsNullOrWhiteSpace(request.EmailFilter))
    {
      query = query.Where(c => c.User.Email.Value.Contains(request.EmailFilter));
    }

    var paginatedCustomers = await PaginatedList<Domain.Entities.Customer>.CreateAsync(query, request.PageNumber, request.PageSize);

    var customerDtos = _mapper.Map<List<CustomerDto>>(paginatedCustomers.Items);

    return new PaginatedList<CustomerDto>(customerDtos, paginatedCustomers.TotalCount, paginatedCustomers.PageNumber, request.PageSize);
  }
}
