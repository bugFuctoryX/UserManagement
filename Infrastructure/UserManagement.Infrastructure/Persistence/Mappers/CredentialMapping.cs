namespace UserManagement.Infrastructure.Persistence.Mappers;

public static class CredentialMapping
{
  public static UserCredential ToCredential(this CredentialRecord r) => new()
  {
    UserId = r.UserId,
    UserName = r.UserName,
    PasswordHash = r.PasswordHash,
    CreatedAtUtc = DateTime.UtcNow,
    LastLoginAtUtc = null
  };

  public static CredentialRecord ToCredentialRecord(this UserCredential c)
  => new CredentialRecord(
    c.UserId,
    c.UserName,
    c.PasswordHash
  );
}
