using UserManagement.Infrastructure.SeedGenerators;

namespace UserManagement.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddInfrastructor(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddOptions<FileDbOptions>()
      .Bind(configuration.GetSection("FileDb"))
      .Validate(o => !string.IsNullOrWhiteSpace(o.CredentialsPath), "FileDb:CredentialsPath is missing.")
      .Validate(o => !string.IsNullOrWhiteSpace(o.UsersPath), "FileDb:UsersPath is missing.")
      .Validate(o => !string.IsNullOrWhiteSpace(o.UserAuditPath), "FileDb:UserAuditPath is missing.")
      .Validate(o => !string.IsNullOrWhiteSpace(o.Delimiter), "FileDb:Delimiter is missing.")
      .ValidateOnStart();

    services.PostConfigure<FileDbOptions>(o =>
    {
      var baseDir = AppContext.BaseDirectory;

      static string Normalize(string p)
          => p.Replace("/", Path.DirectorySeparatorChar.ToString());

      string MakeAbs(string p)
          => Path.IsPathRooted(p) ? Normalize(p) : Path.Combine(baseDir, Normalize(p));

      o.CredentialsPath = MakeAbs(o.CredentialsPath);
      o.UsersPath = MakeAbs(o.UsersPath);
      o.UserAuditPath = MakeAbs(o.UserAuditPath);
    });

    services.AddSingleton(sp => new CredentialsCsvSerializer(sp.GetRequiredService<IOptions<FileDbOptions>>().Value.Delimiter));
    services.AddSingleton(sp => new UsersCsvSerializer(sp.GetRequiredService<IOptions<FileDbOptions>>().Value.Delimiter));
    services.AddSingleton(sp => new AuditCsvSerializer(sp.GetRequiredService<IOptions<FileDbOptions>>().Value.Delimiter));
    services.AddSingleton<IPasswordHasher, PasswordHasher>();
    services.AddSingleton<IFileDbContext, FileDbContext>();
    services.AddScoped<IAuthenticationService, FileAuthenticationService>();
    services.AddScoped<IFileUserRepository, FileUserRepository>();
    services.AddScoped<IFileAuditWriter, FileAuditWriter>();
    services.AddScoped<IFileCredentialRepository, FileCredentialRepository>();
    services.AddScoped<ISeedGenerator, SeedGenerator>();
    services.AddHostedService<SeedDataHostedService>();
    return services;
  }
}
