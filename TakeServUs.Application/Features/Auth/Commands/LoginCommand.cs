using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Application.DTOs.Auth;

namespace TakeServUs.Application.Features.Auth.Commands;

public class LoginCommand : IRequest<LoginResponse>
{
  public required string Email { get; set; }
  public required string Password { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
  private readonly IApplicationDbContext _context;
  private readonly IPasswordHasher _passwordHasher;
  private readonly IJwtTokenGenerator _jwtTokenGenerator;

  public LoginCommandHandler(
      IApplicationDbContext context,
      IPasswordHasher passwordHasher,
      IJwtTokenGenerator jwtTokenGenerator)
  {
    _context = context;
    _passwordHasher = passwordHasher;
    _jwtTokenGenerator = jwtTokenGenerator;
  }

  public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    // 1. Find the user by email
    var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Email.Value == request.Email.ToLower(), cancellationToken);

    if (user == null)
    {
      // For security, use a generic error message
      throw new UnauthorizedAccessException("Invalid email or password.");
    }

    // 2. Verify the password
    var isPasswordValid = _passwordHasher.Verify(request.Password, user.PasswordHash);
    if (!isPasswordValid)
    {
      throw new UnauthorizedAccessException("Invalid email or password.");
    }

    // 3. Generate JWT Token
    var token = _jwtTokenGenerator.GenerateToken(user);

    // 4. Return the response
    return new LoginResponse
    {
      UserId = user.Id,
      CompanyId = user.CompanyId,
      FullName = user.FullName.ToString(),
      Role = user.Role.ToString(),
      Token = token
    };
  }
}

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
  public LoginCommandValidator()
  {
    RuleFor(x => x.Email).NotEmpty().EmailAddress();
    RuleFor(x => x.Password).NotEmpty();
  }
}
