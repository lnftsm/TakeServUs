using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.Features.Technicians.Queries;

public class GetTechniciansWithPaginationQuery : IRequest<PaginatedList<TechnicianDto>>
{
  public Guid CompanyId { get; set; }
  public string? NameFilter { get; set; }
  public bool? IsAvailableFilter { get; set; }
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 10;
}

public class GetTechniciansWithPaginationQueryHandler : IRequestHandler<GetTechniciansWithPaginationQuery, PaginatedList<TechnicianDto>>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public GetTechniciansWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<PaginatedList<TechnicianDto>> Handle(GetTechniciansWithPaginationQuery request, CancellationToken cancellationToken)
  {
    IQueryable<Technician> query = _context.Technicians
        .AsNoTracking()
        .Where(t => t.CompanyId == request.CompanyId)
        .Include(t => t.User);

    if (!string.IsNullOrWhiteSpace(request.NameFilter))
    {
      query = query.Where(t => t.User.FullName.FirstName.Contains(request.NameFilter) || t.User.FullName.LastName.Contains(request.NameFilter));
    }

    if (request.IsAvailableFilter.HasValue)
    {
      query = query.Where(t => t.IsAvailable == request.IsAvailableFilter.Value);
    }

    var paginatedTechnicians = await PaginatedList<Domain.Entities.Technician>.CreateAsync(query, request.PageNumber, request.PageSize);

    var technicianDtos = _mapper.Map<List<TechnicianDto>>(paginatedTechnicians.Items);

    return new PaginatedList<TechnicianDto>(technicianDtos, paginatedTechnicians.TotalCount, paginatedTechnicians.PageNumber, request.PageSize);
  }
}
