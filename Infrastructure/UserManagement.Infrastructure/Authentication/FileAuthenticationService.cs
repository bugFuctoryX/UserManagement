namespace UserManagement.Infrastructure.Authentication;

public class FileAuthenticationService : IAuthenticationService
{
  private readonly IFileCredentialRepository _credentials;
  private readonly IFileUserRepository _users;
  private readonly IPasswordHasher _hasher;

  public FileAuthenticationService(
      IFileCredentialRepository credentials,
      IFileUserRepository users,
      IPasswordHasher hasher)
  {
    _credentials = credentials;
    _users = users;
    _hasher = hasher;
  }

  public async Task<User?> LoginAsync(string username, string password, CancellationToken ct)
  {
    if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
      return null;

    var cred = await _credentials.GetByUsernameAsync(username, ct);
    if (cred is null)
      return null;

    if (!_hasher.Verify(password, cred.PasswordHash))
      return null;

    return await _users.GetByIdAsync(cred.UserId, ct);
  }
}
