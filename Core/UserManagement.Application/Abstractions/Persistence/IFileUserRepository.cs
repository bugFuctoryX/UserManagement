namespace UserManagement.Application.Abstractions.Persistence
{
  public interface IFileUserRepository
  {
    Task<User?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<User?> GetByUsernameAsync(string username, CancellationToken ct);
    Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct);
    Task UpdateAsync(User user, CancellationToken ct);
    Task<bool> DeleteByIdAsync(Guid userId, CancellationToken ct);
    Task<Guid> AddUserAsync(User user, CancellationToken ct);
  }
}
