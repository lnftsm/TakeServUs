using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.Features.Jobs.Queries;

public class GetJobByIdQuery : IRequest<JobDto>
{
  public Guid Id { get; set; }
}

public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, JobDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public GetJobByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<JobDto> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
  {
    var job = await _context.Jobs
        .AsNoTracking()
        .Include(j => j.Customer).ThenInclude(c => c.User) // Eager load related data
        .FirstOrDefaultAsync(j => j.Id == request.Id, cancellationToken);

    if (job == null)
    {
      throw new NotFoundException(nameof(Job), request.Id);
    }

    return _mapper.Map<JobDto>(job);
  }
}
