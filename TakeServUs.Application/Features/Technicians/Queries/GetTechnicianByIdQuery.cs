using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;

namespace TakeServUs.Application.Features.Technicians.Queries;

public class GetTechnicianByIdQuery : IRequest<TechnicianDto>
{
  public Guid Id { get; set; }
}

public class GetTechnicianByIdQueryHandler : IRequestHandler<GetTechnicianByIdQuery, TechnicianDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public GetTechnicianByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<TechnicianDto> Handle(GetTechnicianByIdQuery request, CancellationToken cancellationToken)
  {
    var technician = await _context.Technicians
        .AsNoTracking()
        .Include(t => t.User)
        .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

    if (technician == null)
    {
      throw new NotFoundException(nameof(Domain.Entities.Technician), request.Id);
    }

    return _mapper.Map<TechnicianDto>(technician);
  }
}
