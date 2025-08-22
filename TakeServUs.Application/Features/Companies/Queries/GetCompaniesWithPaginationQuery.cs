using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.DTOs;
using TakeServUs.Application.Common.Models;
using TakeServUs.Application.Common.Interfaces;

namespace TakeServUs.Application.Features.Companies.Queries;

public class GetCompaniesWithPaginationQuery : IRequest<PaginatedList<CompanyDto>>
{
  public string? NameFilter { get; set; }
  public string? CityFilter { get; set; }
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 10;
  public string SortBy { get; set; } = "Name";
  public string SortDirection { get; set; } = "ASC";
}

public class GetCompaniesWithPaginationQueryHandler : IRequestHandler<GetCompaniesWithPaginationQuery, PaginatedList<CompanyDto>>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public GetCompaniesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<PaginatedList<CompanyDto>> Handle(GetCompaniesWithPaginationQuery request, CancellationToken cancellationToken)
  {
    IQueryable<Domain.Entities.Company> query = _context.Companies.AsNoTracking();

    // Filtreleme (Filtering)
    if (!string.IsNullOrWhiteSpace(request.NameFilter))
    {
      query = query.Where(c => c.Name.Contains(request.NameFilter));
    }
    if (!string.IsNullOrWhiteSpace(request.CityFilter))
    {
      query = query.Where(c => c.Address.City.Contains(request.CityFilter));
    }

    // Sıralama (Sorting) - Bu kısım daha dinamik hale getirilebilir.
    // Örnek olarak sadece isme göre sıralama eklenmiştir.
    bool isDescending = request.SortDirection.Equals("DESC", StringComparison.OrdinalIgnoreCase);
    query = request.SortBy.ToLower() switch
    {
      "name" => isDescending ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name),
      "city" => isDescending ? query.OrderByDescending(c => c.Address.City) : query.OrderBy(c => c.Address.City),
      _ => query.OrderBy(c => c.Name)
    };

    // Sayfalama (Pagination)
    var paginatedCompanies = await PaginatedList<Domain.Entities.Company>.CreateAsync(query, request.PageNumber, request.PageSize);

    // Entity listesini DTO listesine çevirme
    var companyDtos = _mapper.Map<List<CompanyDto>>(paginatedCompanies.Items);

    return new PaginatedList<CompanyDto>(companyDtos, paginatedCompanies.TotalCount, paginatedCompanies.PageNumber, request.PageSize);
  }
}
