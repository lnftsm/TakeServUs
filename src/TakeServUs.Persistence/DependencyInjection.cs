using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TakeServUs.Application.Interfaces.Persistence;
using TakeServUs.Persistence.DbContexts;
using TakeServUs.Persistence.Repositories;

namespace TakeServUs.Persistence;

public static class DependencyInjection
{
  public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext<ApplicationDbContext>(options =>
      options.UseNpgsql(
          configuration.GetConnectionString("DefaultConnection"),
          npgSqlOptions =>
          {
            npgSqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
            npgSqlOptions.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorCodesToAdd: null);
          }).UseSnakeCaseNamingConvention());

    services.AddScoped<IApplicationDbContext>(provider =>
        provider.GetRequiredService<ApplicationDbContext>());

    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    services.AddScoped<IUnitOfWork, UnitOfWork>();

    return services;
  }
}
