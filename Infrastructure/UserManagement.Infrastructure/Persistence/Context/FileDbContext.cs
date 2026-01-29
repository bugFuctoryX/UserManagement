namespace UserManagement.Infrastructure.Persistence.Context;

internal class FileDbContext(
  IOptions<FileDbOptions> _options, 
  CredentialsCsvSerializer _credentialsSerializer, 
  UsersCsvSerializer _userSerializer, 
  AuditCsvSerializer _auditSerializer) : IFileDbContext
{
  /// <summary>
  /// Ensures thread-safe, atomic access to the file-based storage by allowing only one concurrent
  /// read/write operation at a time. This prevents race conditions during the typical
  /// read-modify-write workflow (e.g., two requests overwriting each other’s changes), keeps
  /// multiple related files (credentials/users/audit) consistent within a single update, and avoids
  /// partially written or interleaved file content under concurrent requests. Using SemaphoreSlim
  /// enables asynchronous waiting (WaitAsync) without blocking threads and supports cancellation.
  /// </summary>
  private readonly SemaphoreSlim _lock = new(1, 1);

  public async Task AppendAuditAsync(UserAuditRecord entry, CancellationToken ct)
  {
    await _lock.WaitAsync(ct);
    try
    {
      EnsureDirectory(_options.Value.UserAuditPath);

      if (!File.Exists(_options.Value.UserAuditPath))
      {
        var headerOnly = _auditSerializer.SerializeHeaderOnly();
        await File.WriteAllTextAsync(_options.Value.UserAuditPath, headerOnly, _options.Value.Encoding, ct);
      }

      var line = _auditSerializer.SerializeLine(entry);
      await File.AppendAllTextAsync(_options.Value.UserAuditPath, line, _options.Value.Encoding, ct);
    }
    finally
    {
      _lock.Release();
    }
  }

  public Task<IReadOnlyList<CredentialRecord>> ReadCredentialsAsync(CancellationToken ct) => ReadAsync(_options.Value.CredentialsPath, _credentialsSerializer, ct);

  public Task<IReadOnlyList<UserProfileRecord>> ReadUsersAsync(CancellationToken ct) => ReadAsync(_options.Value.UsersPath, _userSerializer, ct);

  public async Task<TResult> WithWriteLockAsync<TResult>(Func<FileDbState, Task<TResult>> action, CancellationToken ct)
  {
    await _lock.WaitAsync(ct);
    try
    {
      var creds = (await ReadAsyncUnlocked(_options.Value.CredentialsPath, _credentialsSerializer, ct)).ToList();
      var users = (await ReadAsyncUnlocked(_options.Value.UsersPath, _userSerializer, ct)).ToList();

      var state = new FileDbState(creds, users);

      var result = await action(state);

      await WriteAsyncUnlocked(_options.Value.CredentialsPath, _credentialsSerializer, state.Credentials, ct);
      await WriteAsyncUnlocked(_options.Value.UsersPath, _userSerializer, state.Users, ct);

      return result;
    }
    finally
    {
      _lock.Release();
    }
  }

  public Task WriteCredentialsAsync(IEnumerable<CredentialRecord> items, CancellationToken ct) => WriteAsync(_options.Value.CredentialsPath, _credentialsSerializer, items, ct);

  public Task WriteUsersAsync(IEnumerable<UserProfileRecord> items, CancellationToken ct) => WriteAsync(_options.Value.UsersPath, _userSerializer, items, ct);


  private async Task<IReadOnlyList<T>> ReadAsyncUnlocked<T>(string path, ICsvSerializer<T> serializer, CancellationToken ct)
  {
    EnsureDirectory(path);

    if (!File.Exists(path))
    {
      var headerOnly = serializer.SerializeHeaderOnly();
      await File.WriteAllTextAsync(path, headerOnly, _options.Value.Encoding, ct);
      return Array.Empty<T>();
    }

    var content = await File.ReadAllTextAsync(path, _options.Value.Encoding, ct);
    return serializer.Deserialize(content);
  }

  private static void EnsureDirectory(string filePath)
  {
    var dir = Path.GetDirectoryName(filePath);
    if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
      Directory.CreateDirectory(dir);
  }

  private async Task WriteAsyncUnlocked<T>(string path, ICsvSerializer<T> serializer, IEnumerable<T> items, CancellationToken ct)
  {
    EnsureDirectory(path);

    var temp = path + ".tmp";
    var bak = path + ".bak";

    var content = serializer.Serialize(items);

    await File.WriteAllTextAsync(temp, content, _options.Value.Encoding, ct);

    if (File.Exists(path))
      File.Copy(path, bak, overwrite: true);

    File.Move(temp, path, overwrite: true);
  }

  private async Task<IReadOnlyList<T>> ReadAsync<T>(string path, ICsvSerializer<T> serializer, CancellationToken ct)
  {
    await _lock.WaitAsync(ct);
    try
    {
      return await ReadAsyncUnlocked(path, serializer, ct);
    }
    finally
    {
      _lock.Release();
    }
  }

  private async Task WriteAsync<T>(string path, ICsvSerializer<T> serializer, IEnumerable<T> items, CancellationToken ct)
  {
    await _lock.WaitAsync(ct);
    try
    {
      await WriteAsyncUnlocked(path, serializer, items, ct);
    }
    finally
    {
      _lock.Release();
    }
  }
}