using AutoMapper;
using MediatR;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Application.Features.Companies.Commands;

public class UpdateCompanyCommand : IRequest<CompanyDto>
{
  public Guid Id { get; set; }
  public required string Name { get; set; }
  public required Address Address { get; set; }
  public required PhoneNumber PhoneNumber { get; set; }
  public required string Email { get; set; }
  public string? Website { get; set; }
}

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CompanyDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public UpdateCompanyCommandHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<CompanyDto> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
  {
    var company = await _context.Companies.FindAsync(new object[] { request.Id }, cancellationToken);

    if (company == null)
    {
      throw new NotFoundException(nameof(Domain.Entities.Company), request.Id);
    }

    // Update properties
    company.Name = request.Name;
    company.Address = request.Address;
    company.PhoneNumber = request.PhoneNumber;
    company.Email = new Email(request.Email);
    company.Website = request.Website;

    await _context.SaveChangesAsync(cancellationToken);

    return _mapper.Map<CompanyDto>(company);
  }
}
