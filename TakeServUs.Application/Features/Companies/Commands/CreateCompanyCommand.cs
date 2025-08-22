using AutoMapper;
using FluentValidation;
using MediatR;
using TakeServUs.Application.Common.Exceptions;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.ValueObjects;

namespace TakeServUs.Application.Features.Companies.Commands;

public class CreateCompanyCommand : IRequest<CompanyDto>
{
  public required string Name { get; set; }
  public required Address Address { get; set; }
  public required PhoneNumber PhoneNumber { get; set; }
  public required string Email { get; set; }
  public string? Website { get; set; }
  public Currency Currency { get; set; } = Currency.TRY;

  // We also need information for the first user (Company Owner)
  public required string OwnerFirstName { get; set; }
  public required string OwnerLastName { get; set; }
  public required string OwnerPassword { get; set; }
}

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;
  // In a real app, you would inject a password hashing service
  // private readonly IPasswordHasher _passwordHasher;

  public CreateCompanyCommandHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
  {
    // In a real app, you would check if the email is already taken.

    var company = new Domain.Entities.Company
    {
      Name = request.Name,
      Address = request.Address,
      PhoneNumber = request.PhoneNumber,
      Email = new Email(request.Email), // Using Value Object
      Website = request.Website,
      Currency = request.Currency,
      // Set default subscription plan info
      SubscriptionPlan = "FreeTier",
      SubscriptionExpiry = DateTime.UtcNow.AddDays(30)
    };

    _context.Companies.Add(company);

    // Create the initial user for this company
    var ownerUser = new Domain.Entities.User
    {
      CompanyId = company.Id,
      FullName = new FullName(request.OwnerFirstName, request.OwnerLastName),
      Email = new Email(request.Email),
      PhoneNumber = request.PhoneNumber,
      PasswordHash = "hashed_" + request.OwnerPassword, // IMPORTANT: Use a real password hasher here!
      Role = Domain.Entities.UserRole.CompanyOwner,
      EmailConfirmed = true // Or send a confirmation email
    };

    _context.Users.Add(ownerUser);

    await _context.SaveChangesAsync(cancellationToken);

    return _mapper.Map<CompanyDto>(company);
  }
}

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
  public CreateCompanyCommandValidator()
  {
    RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
    RuleFor(x => x.Email).NotEmpty().EmailAddress();
    RuleFor(x => x.Address).NotNull();
    RuleFor(x => x.PhoneNumber).NotNull();

    RuleFor(x => x.OwnerFirstName).NotEmpty().MaximumLength(100);
    RuleFor(x => x.OwnerLastName).NotEmpty().MaximumLength(100);
    RuleFor(x => x.OwnerPassword).NotEmpty().MinimumLength(8);
  }
}
