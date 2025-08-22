using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TakeServUs.Application.Common.Behaviors;

namespace TakeServUs.Application;

public static class ApplicationServiceCollectionExtensions
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    // Register AutoMapper
    services.AddAutoMapper(Assembly.GetExecutingAssembly());

    // Register FluentValidation
    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    // Register MediatR
    services.AddMediatR(cfg =>
    {
      cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
      // Add pipeline behaviors
      cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    });

    return services;
  }
}
