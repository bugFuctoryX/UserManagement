namespace UserManagement.Application.Features.Users.Queries.GetAll;

internal sealed class GetUsersHandler(IFileUserRepository _users)
  : IRequestHandler<GetUsersQuery, Result<IReadOnlyList<GetUsersResponse>>>
{
  public async Task<Result<IReadOnlyList<GetUsersResponse>>> Handle(GetUsersQuery request, CancellationToken ct)
  {
    var users = await _users.GetAllAsync(ct);
    var usersResponse = users.Select(user => user.ToUserListItem()).ToList();

    return Result<IReadOnlyList<GetUsersResponse>>.Ok(usersResponse);
  }
}
