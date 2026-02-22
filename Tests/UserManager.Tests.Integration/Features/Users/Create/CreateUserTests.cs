namespace UserManager.Tests.Integration.Features.Users.CreateUser;

[Collection(ApiCollection.Name)]
public class CreateUserTests
{
  private readonly HttpClient _client;
  private readonly CreateUserApi _api;
  public CreateUserTests(ApiFixture fixture)
  {
    _client = fixture.Client;
    _api = new CreateUserApi(fixture.Client);
  }

  [Fact]
  public async Task CreateUser_WithValidRequest_ReturnsSuccess()
  {
    // Arrange
    await AuthSession.SignInAsAdminAsync(_client);

    var req = CreateUserRequests.Valid();

    // Act
    var response = await _api.CreateAsync(req);

    Assert.True(
      response.StatusCode is HttpStatusCode.Created or HttpStatusCode.OK,
      $"Expected 200/201 but got {(int)response.StatusCode} {response.StatusCode}. Body: {await response.Content.ReadAsStringAsync()}");
  }
}
