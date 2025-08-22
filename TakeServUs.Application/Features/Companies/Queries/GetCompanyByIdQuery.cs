using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;

namespace TakeServUs.Application.Features.Companies.Queries;

public class GetCompanyByIdQuery : IRequest<CompanyDto>
{
  public Guid Id { get; set; }
}

public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public GetCompanyByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<CompanyDto> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
  {
    var company = await _context.Companies
        .AsNoTracking() // Read-only operation
        .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

    if (company == null)
    {
      throw new NotFoundException(nameof(Domain.Entities.Company), request.Id);
    }

    return _mapper.Map<CompanyDto>(company);
  }
}
