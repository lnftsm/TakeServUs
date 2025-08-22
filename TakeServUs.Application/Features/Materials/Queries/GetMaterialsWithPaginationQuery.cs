using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.Features.Materials.Queries;

public class GetMaterialsWithPaginationQuery : IRequest<PaginatedList<MaterialDto>>
{
  public Guid CompanyId { get; set; }
  public string? NameFilter { get; set; }
  public string? CodeFilter { get; set; }
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 10;
}

public class GetMaterialsWithPaginationQueryHandler : IRequestHandler<GetMaterialsWithPaginationQuery, PaginatedList<MaterialDto>>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public GetMaterialsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<PaginatedList<MaterialDto>> Handle(GetMaterialsWithPaginationQuery request, CancellationToken cancellationToken)
  {
    IQueryable<Material> query = _context.Materials
        .AsNoTracking()
        .Where(m => m.CompanyId == request.CompanyId);

    if (!string.IsNullOrWhiteSpace(request.NameFilter))
    {
      query = query.Where(m => m.Name.Contains(request.NameFilter));
    }

    if (!string.IsNullOrWhiteSpace(request.CodeFilter))
    {
      query = query.Where(m => m.Code.Contains(request.CodeFilter));
    }

    var paginatedMaterials = await PaginatedList<Domain.Entities.Material>.CreateAsync(query, request.PageNumber, request.PageSize);

    var materialDtos = _mapper.Map<List<MaterialDto>>(paginatedMaterials.Items);

    return new PaginatedList<MaterialDto>(materialDtos, paginatedMaterials.TotalCount, paginatedMaterials.PageNumber, request.PageSize);
  }
}
