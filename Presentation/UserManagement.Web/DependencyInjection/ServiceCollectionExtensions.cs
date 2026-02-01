using Microsoft.AspNetCore.Authentication.Cookies;

namespace UserManagement.Web.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration cofiguration)
  {
    services.AddHttpContextAccessor();
    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
          options.LoginPath = "/login";
          options.LogoutPath = "/logout";
          options.AccessDeniedPath = "/login";
          options.AccessDeniedPath = "/access-denied";
          options.SlidingExpiration = true;
        });

    services.AddAuthorization();
    services.AddCascadingAuthenticationState();

    services.AddScoped<IWebAuthService, WebCookieAuthService>();
    return services;
  }
}