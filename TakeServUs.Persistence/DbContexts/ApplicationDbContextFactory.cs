using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TakeServUs.Persistence.DbContexts
{
  public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
  {
    public ApplicationDbContext CreateDbContext(string[] args)
    {
      var configuration = new ConfigurationBuilder()
          .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../TakeServUs.API"))
          .AddJsonFile("appsettings.json")
          .Build();

      var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
      optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

      return new ApplicationDbContext(optionsBuilder.Options);
    }
  }
}
