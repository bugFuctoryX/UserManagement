namespace UserManagement.Application.Features.Users.Queries.GetAll;

public sealed record GetUsersResponse(Guid UserId,
  string UserName,
  string FirstName,
  string LastName,
  string Email,
  DateOnly BirthDate,
  string BirthPlace,
  string City);
