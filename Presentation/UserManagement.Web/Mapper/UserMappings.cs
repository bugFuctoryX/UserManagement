namespace UserManagement.Web.Mapper;

public static class UserMappings
{
  public static UserModel ToUserModel(this GetUsersResponse src) => new(
      UserId: src.UserId,
      UserName: src.UserName,
      Email: src.Email,
      BirthDate: src.BirthDate,
      BirthPlace: src.BirthPlace,
      City: src.City,
      FullName: $"{src.FirstName} {src.LastName}"
  );

  public static UpdateUserModel ToUserModel(this GetUserByIdResponse src) => new()
    {
      UserId = src.UserId,
      UserName = src.UserName,
      Email = src.Email,
      FirstName = src.FirstName,
      LastName = src.LastName,
      BirthDate = src.BirthDate,
      BirthPlace = src.BirthPlace,
      City = src.City,
      FullName = src.FullName
    };

  public static UpdateUserCommand ToUpdateUserCommand(this UpdateUserModel model, DateOnly birthDate) => new(
      UserId: model.UserId,
      UserName: model.UserName,
      Email: model.Email,
      Password: "",
      FirstName: model.FirstName,
      LastName: model.LastName,
      BirthDate: birthDate,
      BirthPlace: model.BirthPlace,
      City: model.City,
      FullName: $"{model.FirstName} {model.LastName}"
    );

  public static IReadOnlyList<UserModel> ToUserModels(this IEnumerable<GetUsersResponse> src)
      => src.Select(x => x.ToUserModel()).ToList();
}