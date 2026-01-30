namespace UserManagement.Infrastructure.SeedGenerators;

internal interface ISeedGenerator
{
  Task SeedAsync(CancellationToken ct);
}
