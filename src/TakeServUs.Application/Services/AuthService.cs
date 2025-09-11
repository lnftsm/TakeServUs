using System;
using System.Threading;
using System.Threading.Tasks;
using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Auth;
using TakeServUs.Application.Interfaces.Services;

namespace TakeServUs.Application.Services;

public class AuthService : IAuthService
{
  // Implementation of authentication-related methods
  public Task<ServiceResult<bool>> ChangePasswordAsync(Guid userId, ChangePasswordDto request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> ConfirmEmailAsync(string token, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> ForgotPasswordAsync(ForgotPasswordDto request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<LoginResponseDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> LogoutAsync(Guid userId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<RefreshTokenResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<RegisterResponseDto>> RegisterAsync(RegisterRequestDto request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> ResetPasswordAsync(string token, string newPassword, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> ValidateTokenAsync(string token, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}
