namespace UserManagement.Application.Features.Users.Commands.Delete;

public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<bool>>
{
  private readonly IFileUserRepository _repository;

  public DeleteUserCommandHandler(IFileUserRepository repository)
  {
    _repository = repository;
  }

  public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken ct)
  {
    if (request.UserId == Guid.Empty)
      return Result<bool>.Fail(ErrorType.Validation, "UserId is required.");

    var user = await _repository.GetByIdAsync(request.UserId, ct);
    if (user is null)
      return Result<bool>.Fail(ErrorType.NotFound, "User not found.");

    var deleted = await _repository.DeleteByIdAsync(request.UserId, ct);
    if (!deleted)
      return Result<bool>.Fail(ErrorType.Conflict, "Delete failed. The user may have been deleted already.");

    return Result<bool>.Ok(true);
  }
}
