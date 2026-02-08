namespace UserManager.Tests.Integration.Fixtures;

[CollectionDefinition(Name)]
public class ApiCollection : ICollectionFixture<ApiFixture>
{
  public const string Name = "Blazor Server";
}
