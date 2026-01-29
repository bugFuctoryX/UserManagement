namespace UserManagement.Infrastructure.Persistence.Records;

public sealed record CredentialRecord(
  Guid UserId,
  string UserName,
  string PasswordHash
);
