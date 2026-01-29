namespace UserManagement.Application.Features.Authentication.Login;

public sealed record LoginResponse(
  Guid UserId,
  string UserName,
  string FirstName,
  string LastName
);
