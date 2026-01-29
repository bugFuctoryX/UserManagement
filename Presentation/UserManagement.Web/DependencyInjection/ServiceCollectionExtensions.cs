namespace UserManagement.Web.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration cofiguration)
  {
    return services;
  }
}
