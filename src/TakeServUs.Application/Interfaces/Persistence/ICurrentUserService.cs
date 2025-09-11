using System.Security.Claims;

namespace TakeServUs.Application.Interfaces.Infrastructure;

public interface ICurrentUserService
{
  Guid? UserId { get; }
  string? UserIdString { get; }
  string? UserName { get; }
  string? Email { get; }
  string? FullName { get; }
  string? FirstName { get; }
  string? LastName { get; }
  IReadOnlyList<string> Roles { get; }
  IReadOnlyList<string> Permissions { get; }
  bool IsAuthenticated { get; }
  bool IsInRole(string role);
  bool IsInAnyRole(params string[] roles);
  bool IsInAllRoles(params string[] roles);
  bool HasPermission(string permission);
  bool HasAnyPermission(params string[] permissions);
  bool HasAllPermissions(params string[] permissions);
  string? GetClaim(string claimType);
  IReadOnlyList<string> GetClaims(string claimType);
  IReadOnlyList<Claim> Claims { get; }
  Guid? TenantId { get; }
  string? IpAddress { get; }
  string? UserAgent { get; }
  string? Token { get; }
  DateTime? TokenExpiry { get; }
}
