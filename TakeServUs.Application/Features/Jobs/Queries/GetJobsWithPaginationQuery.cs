using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.Features.Jobs.Queries;

public class GetJobsWithPaginationQuery : IRequest<PaginatedList<JobDto>>
{
  public Guid CompanyId { get; set; }
  public string? TitleFilter { get; set; }
  public Guid? CustomerIdFilter { get; set; }
  public JobStatusType? StatusFilter { get; set; }
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 10;
}

public class GetJobsWithPaginationQueryHandler : IRequestHandler<GetJobsWithPaginationQuery, PaginatedList<JobDto>>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public GetJobsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<PaginatedList<JobDto>> Handle(GetJobsWithPaginationQuery request, CancellationToken cancellationToken)
  {
    IQueryable<Job> query = _context.Jobs
        .AsNoTracking()
        .Where(j => j.CompanyId == request.CompanyId)
        .Include(j => j.Customer).ThenInclude(c => c.User);

    if (!string.IsNullOrWhiteSpace(request.TitleFilter))
    {
      query = query.Where(j => j.Title.Contains(request.TitleFilter));
    }

    if (request.CustomerIdFilter.HasValue)
    {
      query = query.Where(j => j.CustomerId == request.CustomerIdFilter.Value);
    }

    if (request.StatusFilter.HasValue)
    {
      query = query.Where(j => j.CurrentStatus == request.StatusFilter.Value);
    }

    var paginatedJobs = await PaginatedList<Job>.CreateAsync(query, request.PageNumber, request.PageSize);

    var jobDtos = _mapper.Map<List<JobDto>>(paginatedJobs.Items);

    return new PaginatedList<JobDto>(jobDtos, paginatedJobs.TotalCount, paginatedJobs.PageNumber, request.PageSize);
  }
}
