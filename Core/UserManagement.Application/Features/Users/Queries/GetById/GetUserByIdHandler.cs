namespace UserManagement.Application.Features.Users.Queries.GetById;

internal sealed class GetUserByIdHandler(IFileUserRepository userRepository)
    : IRequestHandler<GetUserByIdQuery, Result<GetUserByIdResponse>>
{
  public async Task<Result<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken ct)
  {
    var user = await userRepository.GetByIdAsync(request.UserId, ct);

    if (user is null)
      return Result<GetUserByIdResponse>.Fail(ErrorType.NotFound, "User not found.");

    return Result<GetUserByIdResponse>.Ok(user.ToResponse());
  }
}