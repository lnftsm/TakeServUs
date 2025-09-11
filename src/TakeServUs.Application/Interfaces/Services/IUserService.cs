using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.User;
using TakeServUs.Domain.Enums;

namespace TakeServUs.Application.Interfaces.Services;

public interface IUserService
{
  Task<ServiceResult<UserDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<ServiceResult<PagedResponse<UserDto>>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default);
  Task<ServiceResult<UserDto>> CreateAsync(UserCreateDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<UserDto>> UpdateAsync(Guid id, UserUpdateDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

  // User specific operations
  Task<ServiceResult<UserDto>> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
  Task<ServiceResult<UserProfileDto>> GetProfileAsync(Guid userId, CancellationToken cancellationToken = default);
  Task<ServiceResult<UserProfileDto>> UpdateProfileAsync(Guid userId, UserProfileDto dto, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> UpdateProfileImageAsync(Guid userId, Stream imageStream, string fileName, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> UpdateRoleAsync(Guid userId, UserRole newRole, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> ConfirmPhoneNumberAsync(Guid userId, string code, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> UpdatePreferencesAsync(Guid userId, string language, string theme, CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<UserDto>>> GetUsersByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default);
  Task<ServiceResult<IEnumerable<UserDto>>> GetUsersByRoleAsync(UserRole role, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> BlockUserAsync(Guid userId, string reason, CancellationToken cancellationToken = default);
  Task<ServiceResult<bool>> UnblockUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
