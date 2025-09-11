using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.Auth;

namespace TakeServUs.Application.Interfaces.Services;

public interface IAuthService
{
  Task<ServiceResult<LoginResponseDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default);
  Task<ServiceResult<RegisterResponseDto>> RegisterAsync(RegisterRequestDto request, CancellationToken cancellationToken = default);
  Task<ServiceResult<RefreshTokenResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> LogoutAsync(Guid userId, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> ChangePasswordAsync(Guid userId, ChangePasswordDto request, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> ForgotPasswordAsync(ForgotPasswordDto request, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> ResetPasswordAsync(string token, string newPassword, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> ConfirmEmailAsync(string token, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> ValidateTokenAsync(string token, CancellationToken cancellationToken = default);
}
