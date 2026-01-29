namespace UserManagement.Application.Abstractions.Authentication;

public interface IPasswordHasher
{
  /// <summary>
  /// Hashes a plaintext password using a strong, salted algorithm.
  /// </summary>
  string Hash(string password);

  /// <summary>
  /// Verifies a plaintext password against a stored hash.
  /// </summary>
  bool Verify(string password, string storedHash);
}
