namespace UserManagement.Application.Abstractions.Authentication;

public interface IAuthenticationService
{
  Task<User?> LoginAsync(string username, string password, CancellationToken ct);
}
