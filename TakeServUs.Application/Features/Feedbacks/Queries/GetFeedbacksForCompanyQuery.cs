using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.Features.Feedbacks.Queries;

public class GetFeedbacksForCompanyQuery : IRequest<PaginatedList<FeedbackDto>>
{
  public Guid CompanyId { get; set; }
  public int PageNumber { get; set; } = 1;
  public int PageSize { get; set; } = 10;
}

public class GetFeedbacksForCompanyQueryHandler : IRequestHandler<GetFeedbacksForCompanyQuery, PaginatedList<FeedbackDto>>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public GetFeedbacksForCompanyQueryHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<PaginatedList<FeedbackDto>> Handle(GetFeedbacksForCompanyQuery request, CancellationToken cancellationToken)
  {
    var query = _context.Feedbacks
        .AsNoTracking()
        .Include(f => f.Customer)
        .Where(f => f.Customer.CompanyId == request.CompanyId)
        .OrderByDescending(f => f.CreatedAt);

    var paginatedFeedbacks = await PaginatedList<Feedback>.CreateAsync(query, request.PageNumber, request.PageSize);

    var feedbackDtos = _mapper.Map<List<FeedbackDto>>(paginatedFeedbacks.Items);

    return new PaginatedList<FeedbackDto>(feedbackDtos, paginatedFeedbacks.TotalCount, paginatedFeedbacks.PageNumber, request.PageSize);
  }
}
