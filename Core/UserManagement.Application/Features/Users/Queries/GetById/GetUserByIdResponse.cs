namespace UserManagement.Application.Features.Users.Queries.GetById;

public sealed record GetUserByIdResponse(
    Guid UserId,
    string UserName,
    string Email,
    string FirstName,
    string LastName,
    string Password,
    DateOnly BirthDate,
    string BirthPlace,
    string City,
    string FullName
);
