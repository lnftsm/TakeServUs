using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TakeServUs.Application.Common.Interfaces;
using TakeServUs.Persistence.DbContexts;
using TakeServUs.Persistence.Interceptors;
using TakeServUs.Persistence.Repositories;

namespace TakeServUs.Persistence;

public static class DependencyInjection
{
  public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
  {
    // Interceptor'ı kaydet
    services.AddScoped<AuditableEntityInterceptor>();

    // DbContext'i interceptor ile kaydet
    services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
    {
      var auditInterceptor = serviceProvider.GetRequiredService<AuditableEntityInterceptor>();

      options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                 .UseSnakeCaseNamingConvention()
                 .AddInterceptors(auditInterceptor);
    });

    // IApplicationDbContext interface'ini kaydet
    services.AddScoped<IApplicationDbContext>(provider =>
        provider.GetRequiredService<ApplicationDbContext>());

    // Repository Pattern
    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    services.AddScoped<IUnitOfWork, UnitOfWork>();

    return services;
  }
}
