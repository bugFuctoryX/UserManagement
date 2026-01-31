namespace UserManagement.Application.Features.Users.Queries.GetAll;

public sealed record GetUsersQuery : IRequest<Result<IReadOnlyList<GetUsersResponse>>>;