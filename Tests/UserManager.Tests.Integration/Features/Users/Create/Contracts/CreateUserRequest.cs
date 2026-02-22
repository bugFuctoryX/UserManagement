namespace UserManager.Tests.Integration.Features.Users.CreateUser.Contracts;

public sealed record CreateUserRequest(Guid UserId, 
  string UserName,
  string Email,
  string Password,
  string FirstName,
  string LastName,
  DateOnly BirthDate,
  string BirthPlace,
  string City);
