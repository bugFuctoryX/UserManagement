namespace UserManagement.Infrastructure.Persistence.Records;

public sealed record UserProfileRecord(
  Guid UserId,
  string Email,
  string LastName,
  string FirstName,
  DateOnly BirthDate,
  string BirthPlace,
  string City
);
