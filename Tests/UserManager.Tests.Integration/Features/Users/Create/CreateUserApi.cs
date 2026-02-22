namespace UserManager.Tests.Integration.Features.Users.CreateUser;

internal sealed class CreateUserApi(HttpClient _client)
{
  private const string _url = "/users/new";
  public async Task<HttpResponseMessage> CreateAsync(CreateUserRequest createUserRequest)
  {
    var content = MultipartContentFactory.BuildCreateUserMultipart(createUserRequest);

    var result = await DumpMultipartAsync(content);

    return await _client.PostAsync(_url, content);
  }

  public static async Task<Dictionary<string, string>> DumpMultipartAsync(MultipartFormDataContent content)
  {
    var list = new Dictionary<string, string>();
    foreach (var part in content)
    {
      var name = part.Headers.ContentDisposition?.Name?.Trim('"') ?? "<no-name>";
      var value = await part.ReadAsStringAsync();

      list.Add(name, value);
    }
    return list;
  }
}
