namespace UserManager.Tests.Integration.Fixtures;

public sealed class ApiFixture : IAsyncLifetime
{
  public CustomWebApplicationFactory Factory { get; } = new();
  public HttpClient Client { get; private set; } = default!;

  public async Task InitializeAsync()
  {
    Client = Factory.CreateClient(new WebApplicationFactoryClientOptions
    {
      AllowAutoRedirect = false
    });

    using var scope = Factory.Services.CreateScope();
    var ctx = scope.ServiceProvider.GetRequiredService<IFileDbContext>();
    var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

    var seeder = new FileDbSeeder(ctx);
    await seeder.SeedIfEmptyAsync(CancellationToken.None);
  }

  public async Task DisposeAsync()
  {
    Client?.Dispose();
    Factory.Dispose();
  }
}
