namespace UserManagement.Infrastructure.Persistence.Options;

public sealed record FileDbOptions
{
  public string CredentialsPath { get; set; } = default!;
  public string UsersPath { get; set; } = default!;
  public string UserAuditPath { get; set; } = default!;
  public string Delimiter { get; set; } = default!;
  public Encoding Encoding { get; set; } = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
}
