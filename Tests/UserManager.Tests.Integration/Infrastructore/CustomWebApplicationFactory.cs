namespace UserManager.Tests.Integration.Infrastructore;

[Collection(ApiCollection.Name)]
public class CustomWebApplicationFactory : WebApplicationFactory<WebAssemblyMarker>
{
  protected override async void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.UseEnvironment("Test");

    builder.ConfigureTestServices(services =>
    {
      services.PostConfigure<FileDbOptions>(o =>
      {
        o.Delimiter = ";";
        o.Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

        o.CredentialsPath = "App_Data_Test/credentials.csv";
        o.UsersPath = "App_Data_Test/users.csv";
        o.UserAuditPath = "App_Data_Test/user_audit.csv";
      });

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = TestAuthHandler.SchemeName;
        options.DefaultChallengeScheme = TestAuthHandler.SchemeName;
      })
      .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.SchemeName, _ => { });

      services.AddAuthorization();    
    });
  }
}