namespace UserManagement.Infrastructure.Persistence.Context;

public sealed class FileDbState
{
  public IList<CredentialRecord> Credentials { get; }
  public IList<UserProfileRecord> Users { get; }

  public FileDbState(IList<CredentialRecord> credentials, IList<UserProfileRecord> users)
  {
    Credentials = credentials;
    Users = users;
  }
}
