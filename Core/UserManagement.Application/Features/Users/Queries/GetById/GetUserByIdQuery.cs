namespace UserManagement.Application.Features.Users.Queries.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : IRequest<Result<GetUserByIdResponse>>;