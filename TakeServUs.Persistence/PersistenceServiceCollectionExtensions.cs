using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TakeServUs.Persistence.DbContexts;

namespace TakeServUs.Persistence;

public static class PersistenceServiceCollectionExtensions
{
  public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext<ApplicationDbContext>(options =>
    {
      var connStr = configuration.GetConnectionString("DefaultConnection");
      options.UseNpgsql(connStr);
    });

    return services;
  }
}
