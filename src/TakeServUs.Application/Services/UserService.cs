using TakeServUs.Application.Common.Models;
using TakeServUs.Application.DTOs.User;
using TakeServUs.Application.Interfaces.Services;
using TakeServUs.Domain.Enums;

namespace TakeServUs.Application.Services;

public class UserService : IUserService
{
  public Task<ServiceResult<bool>> BlockUserAsync(Guid userId, string reason, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> ConfirmPhoneNumberAsync(Guid userId, string code, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<UserDto>> CreateAsync(UserCreateDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<UserDto>> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<UserDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<PagedResponse<UserDto>>> GetPagedAsync(PagedRequest request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<UserProfileDto>> GetProfileAsync(Guid userId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<UserDto>>> GetUsersByCompanyAsync(Guid companyId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<IEnumerable<UserDto>>> GetUsersByRoleAsync(UserRole role, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> UnblockUserAsync(Guid userId, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<UserDto>> UpdateAsync(Guid id, UserUpdateDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> UpdatePreferencesAsync(Guid userId, string language, string theme, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<UserProfileDto>> UpdateProfileAsync(Guid userId, UserProfileDto dto, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> UpdateProfileImageAsync(Guid userId, Stream imageStream, string fileName, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<ServiceResult<bool>> UpdateRoleAsync(Guid userId, UserRole newRole, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}
