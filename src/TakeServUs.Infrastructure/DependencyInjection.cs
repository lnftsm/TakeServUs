using Microsoft.Extensions.DependencyInjection;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Infrastructure.Services;

namespace TakeServUs.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services)
  {
    services.AddHttpContextAccessor();
    services.AddScoped<ICurrentUserService, CurrentUserService>();
    services.AddSingleton<IDateTime, DateTimeService>();

    return services;
  }
}
