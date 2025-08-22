using AutoMapper;
using FluentValidation;
using MediatR;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Application.Features.Technicians.Commands;

public class CreateTechnicianCommand : IRequest<TechnicianDto>
{
  public Guid CompanyId { get; set; }
  public required string FirstName { get; set; }
  public required string LastName { get; set; }
  public required string Email { get; set; }
  public required PhoneNumber PhoneNumber { get; set; }
  public required string Password { get; set; }
  public string? EmployeeCode { get; set; }
  public string? Specialization { get; set; }
}

public class CreateTechnicianCommandHandler : IRequestHandler<CreateTechnicianCommand, TechnicianDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public CreateTechnicianCommandHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<TechnicianDto> Handle(CreateTechnicianCommand request, CancellationToken cancellationToken)
  {
    // First, create the User entity for the technician
    var newUser = new Domain.Entities.User
    {
      CompanyId = request.CompanyId,
      FullName = new FullName(request.FirstName, request.LastName),
      Email = new Email(request.Email),
      PhoneNumber = request.PhoneNumber,
      PasswordHash = "hashed_" + request.Password, // IMPORTANT: Use a real password hasher!
      Role = Domain.Entities.UserRole.Technician,
      EmailConfirmed = true
    };
    _context.Users.Add(newUser);

    // Then, create the Technician entity linked to the User
    var technician = new Domain.Entities.Technician
    {
      UserId = newUser.Id,
      CompanyId = request.CompanyId,
      EmployeeCode = request.EmployeeCode,
      Specialization = request.Specialization,
      IsAvailable = true
    };
    _context.Technicians.Add(technician);

    await _context.SaveChangesAsync(cancellationToken);

    // Map the result to a DTO
    var technicianDto = _mapper.Map<TechnicianDto>(technician);
    technicianDto.FullName = newUser.FullName.ToString();
    technicianDto.Email = newUser.Email.ToString();

    return technicianDto;
  }
}

public class CreateTechnicianCommandValidator : AbstractValidator<CreateTechnicianCommand>
{
  public CreateTechnicianCommandValidator()
  {
    RuleFor(x => x.CompanyId).NotEmpty();
    RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
    RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
    RuleFor(x => x.Email).NotEmpty().EmailAddress();
    RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
    RuleFor(x => x.PhoneNumber).NotNull();
  }
}
