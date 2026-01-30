namespace UserManagement.Infrastructure.SeedGenerators;

internal sealed class SeedDataHostedService(
   IServiceScopeFactory _scopeFactory) : IHostedService
{
  public async Task StartAsync(CancellationToken cancellationToken)
  {
    try
    {
      using var scope = _scopeFactory.CreateScope();
      var seedGenerator = scope.ServiceProvider.GetRequiredService<ISeedGenerator>();
      await seedGenerator.SeedAsync(cancellationToken);
    }
    catch (Exception ex)
    {
      //ToDoo: Implement proper logging
      string message = ex.Message;
      throw;
    }
  }

  public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
