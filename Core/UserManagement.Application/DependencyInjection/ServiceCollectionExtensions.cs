namespace UserManagement.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

    services.AddMediatR(cfg =>
    {
      cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly);
    });
    services.AddValidatorsFromAssembly(typeof(AssemblyMarker).Assembly);

    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

    return services;
  }
}
