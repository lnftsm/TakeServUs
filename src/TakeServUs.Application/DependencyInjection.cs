using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TakeServUs.Application.Interfaces.Services;
using TakeServUs.Application.Services;

namespace TakeServUs.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    // AutoMapper
    services.AddAutoMapper(Assembly.GetExecutingAssembly());

    // FluentValidation
    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    // MediatR (if you decide to use it later)
    // services.AddMediatR(cfg =>
    // {
    //   cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    //   cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    //   cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    //   cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
    // });

    // Register Services
    services.AddScoped<IAuthService, AuthService>();
    services.AddScoped<ICompanyService, CompanyService>();
    services.AddScoped<ICustomerService, CustomerService>();
    services.AddScoped<IFeedbackService, FeedbackService>();
    services.AddScoped<IInvoiceService, InvoiceService>();
    services.AddScoped<IJobService, JobService>();
    services.AddScoped<ILocationService, LocationService>();
    services.AddScoped<IMaterialService, MaterialService>();
    services.AddScoped<INotificationService, NotificationService>();
    services.AddScoped<IQueuedEmailService, QueuedEmailService>();
    services.AddScoped<IQuoteService, QuoteService>();
    services.AddScoped<ITechnicianService, TechnicianService>();
    services.AddScoped<IUserService, UserService>();
    //services.AddScoped<IReportService, ReportService>();

    return services;
  }
}
