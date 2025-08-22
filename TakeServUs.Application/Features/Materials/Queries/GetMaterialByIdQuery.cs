using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;

namespace TakeServUs.Application.Features.Materials.Queries;

public class GetMaterialByIdQuery : IRequest<MaterialDto>
{
  public Guid Id { get; set; }
}

public class GetMaterialByIdQueryHandler : IRequestHandler<GetMaterialByIdQuery, MaterialDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public GetMaterialByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<MaterialDto> Handle(GetMaterialByIdQuery request, CancellationToken cancellationToken)
  {
    var material = await _context.Materials
        .AsNoTracking()
        .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

    if (material == null)
    {
      throw new NotFoundException(nameof(Domain.Entities.Material), request.Id);
    }

    return _mapper.Map<MaterialDto>(material);
  }
}
