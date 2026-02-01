using UserManagement.Application.Common;

namespace UserManagement.Web.Services.Authentication;

public interface IWebAuthService
{
  Task<Result<bool>> SignInAsync(LoginUser user, CancellationToken ct = default);
  Task SignOutAsync(CancellationToken ct = default);
}