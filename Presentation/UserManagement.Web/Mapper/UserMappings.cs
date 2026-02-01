using UserManagement.Application.Features.Users.Commands.Add;

namespace UserManagement.Web.Mapper;

public static class UserMappings
{
  public static UserGridViewModel ToUserModel(this GetUsersResponse src) => new(
      UserId: src.UserId,
      UserName: src.UserName,
      Email: src.Email,
      BirthDate: src.BirthDate,
      BirthPlace: src.BirthPlace,
      City: src.City,
      FullName: $"{src.FirstName} {src.LastName}"
  );

  public static UserViewModel ToUserModel(this GetUserByIdResponse src) => new()
    {
      UserId = src.UserId,
      UserName = src.UserName,
      Email = src.Email,
      FirstName = src.FirstName,
      LastName = src.LastName,
      Password = src.Password,
      BirthDate = src.BirthDate,
      BirthPlace = src.BirthPlace,
      City = src.City,
      FullName = src.FullName
    };

  public static UpdateUserCommand ToUpdateUserCommand(this UserViewModel model, DateOnly birthDate) => new(
      UserId: model.UserId,
      UserName: model.UserName,
      Email: model.Email,
      Password: model.Password,
      FirstName: model.FirstName,
      LastName: model.LastName,
      BirthDate: birthDate,
      BirthPlace: model.BirthPlace,
      City: model.City,
      FullName: $"{model.FirstName} {model.LastName}"
    );

  public static AddUserCommand ToAddUserCommand(this UserViewModel model, DateOnly birthDate) => new(
    UserId: model.UserId,
    UserName: model.UserName,
    Email: model.Email,
    Password: model.Password,
    FirstName: model.FirstName,
    LastName: model.LastName,
    BirthDate: birthDate,
    BirthPlace: model.BirthPlace,
    City: model.City
  );

  public static IReadOnlyList<UserGridViewModel> ToUserModels(this IEnumerable<GetUsersResponse> src)
      => src.Select(x => x.ToUserModel()).ToList();
}