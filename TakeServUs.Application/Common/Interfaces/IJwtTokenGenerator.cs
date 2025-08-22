using TakeServUs.Domain.Entities;

namespace TakeServUs.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
  string GenerateToken(User user);
}
