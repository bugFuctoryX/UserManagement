namespace UserManagement.Application.Abstractions.Persistence;

public interface IFileCredentialRepository
{
  Task<UserCredential?> GetByUsernameAsync(string username, CancellationToken ct);
  Task<UserCredential?> GetByUserIdAsync(Guid userId, CancellationToken ct);
  Task UpdatePasswordHashAsync(Guid userId, string newHash, CancellationToken ct);
}
