namespace UserManagement.Infrastructure.Authentication.Passwords;
/// <summary>
/// PBKDF2-based password hasher using RFC 2898 (Rfc2898DeriveBytes) with a per-password random salt.
/// 
/// PBKDF2 is a standard key-derivation function designed for securely storing passwords by applying
/// many iterations of HMAC (here SHA-256). The high iteration count makes brute-force and dictionary
/// attacks significantly more expensive than fast hashes.
///
/// The resulting hash is stored in a self-describing format:
/// "PBKDF2$<iterations>$<saltBase64>$<hashBase64>"
/// which enables verification later without additional metadata and allows future iteration tuning.
/// 
/// Verification re-derives the hash using the stored salt and iteration count and compares it using
/// a constant-time comparison to reduce timing-attack risk.
/// </summary>
public class PasswordHasher : IPasswordHasher
{
  private const int SaltSize = 16;
  private const int KeySize = 32;
  private const int Iterations = 100_000;

  private const string Prefix = "PBKDF2";
  private const char Sep = '$';

  public string Hash(string password)
  {
    if (string.IsNullOrWhiteSpace(password))
      throw new ArgumentException("Password must not be empty.", nameof(password));

    var salt = RandomNumberGenerator.GetBytes(SaltSize);
    var hash = Rfc2898DeriveBytes.Pbkdf2(
      password: password,
      salt: salt,
      iterations: Iterations,
      hashAlgorithm: HashAlgorithmName.SHA256,
      outputLength: KeySize);

    return string.Join(Sep, Prefix, Iterations.ToString(), Convert.ToBase64String(salt), Convert.ToBase64String(hash));
  }

  public bool Verify(string password, string storedHash)
  {
    if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(storedHash))
      return false;

    var parts = storedHash.Split(Sep);
    if (parts.Length != 4)
      return false;

    if (!string.Equals(parts[0], Prefix, StringComparison.Ordinal))
      return false;

    if (!int.TryParse(parts[1], out var iterations) || iterations <= 0)
      return false;

    byte[] salt, expected;
    try
    {
      salt = Convert.FromBase64String(parts[2]);
      expected = Convert.FromBase64String(parts[3]);
    }
    catch
    {
      return false;
    }

    var actual = Rfc2898DeriveBytes.Pbkdf2(
      password: password,
      salt: salt,
      iterations: iterations,
      hashAlgorithm: HashAlgorithmName.SHA256,
      outputLength: expected.Length);

    return CryptographicOperations.FixedTimeEquals(actual, expected);
  }
}
