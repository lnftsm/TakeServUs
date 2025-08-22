using MediatR;
using TakeServUs.Application.DTOs;
using TakeServUs.Domain.Entities;
using TakeServUs.Domain.ValueObjects;
using FluentValidation;
using AutoMapper;
using TakeServUs.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Exceptions;

namespace TakeServUs.Application.Features.Customers.Commands;

public class CreateCustomerCommand : IRequest<CustomerDto>
{
  public Guid CompanyId { get; set; }
  public required string FirstName { get; set; }
  public required string LastName { get; set; }
  public required string Email { get; set; }
  public required PhoneNumber PhoneNumber { get; set; }
  public required string Password { get; set; }
  public required Address Address { get; set; }
  public string? CompanyName { get; set; } // Customer's own company, if any
  public string? Notes { get; set; }
  public bool IsVip { get; set; }
}

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
{
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public CreateCustomerCommandHandler(IApplicationDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
  {
    // First, create the User entity for the customer
    var newUser = new Domain.Entities.User
    {
      CompanyId = request.CompanyId,
      FullName = new FullName(request.FirstName, request.LastName),
      Email = new Email(request.Email),
      PhoneNumber = request.PhoneNumber,
      PasswordHash = "hashed_" + request.Password, // IMPORTANT: Use a real password hasher!
      Role = Domain.Entities.UserRole.Customer,
      EmailConfirmed = true
    };

    _context.Users.Add(newUser);

    // Then, create the Customer entity linked to the User
    var customer = new Domain.Entities.Customer
    {
      UserId = newUser.Id,
      CompanyId = request.CompanyId,
      Address = request.Address,
      CompanyName = request.CompanyName,
      Notes = request.Notes,
      IsVip = request.IsVip
    };

    _context.Customers.Add(customer);
    await _context.SaveChangesAsync(cancellationToken);

    // Map the result to a DTO
    var customerDto = _mapper.Map<CustomerDto>(customer);
    // Manually populate fields from the new User
    customerDto.FullName = newUser.FullName.ToString();
    customerDto.Email = newUser.Email.ToString();
    customerDto.PhoneNumber = newUser.PhoneNumber.ToString();

    return customerDto;
  }
}

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
  public CreateCustomerCommandValidator()
  {
    RuleFor(x => x.CompanyId).NotEmpty();
    RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
    RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
    RuleFor(x => x.Email).NotEmpty().EmailAddress();
    RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
    RuleFor(x => x.Address).NotNull();
    RuleFor(x => x.PhoneNumber).NotNull();
  }
}
