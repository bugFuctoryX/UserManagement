using System.Globalization;
using UserManagement.Application.Features.Users.Commands.Update;
using UserManagement.Application.Features.Users.Queries.ExportUsersXml;
using UserManagement.Application.Features.Users.Queries.ExportUsersXml.Xml;
using UserManagement.Application.Features.Users.Queries.GetById;

namespace UserManagement.Application.Mapping;

public static class UserMappings
{
  public static LoginResponse ToLoginResponse(this User user) => new(
    UserId: user.Id,
    UserName: user.Credential.UserName,
    FirstName: user.Profile.FirstName,
    LastName: user.Profile.LastName,
    Email: user.Profile.Email
   );

  public static GetUsersResponse ToUserListItem(this User user) => new(
  UserId: user.Id,
  UserName: user.Credential.UserName,
  FirstName: user.Profile.FirstName,
  LastName: user.Profile.LastName,
  Email: user.Profile.Email,
  BirthDate: user.Profile.BirthDate,
  BirthPlace: user.Profile.BirthPlace,
  City: user.Profile.City
 );

  public static User ToUser(this UpdateUserCommand src, string passwordHash, DateTime utcNow) => new()
  {
    Id = src.UserId,
    Credential = new UserCredential
    {
      UserId = src.UserId,
      UserName = src.UserName,
      PasswordHash = passwordHash,
      CreatedAtUtc = utcNow
    },
    Profile = new UserProfile
    {
      UserId = src.UserId,
      Email = src.Email,
      FirstName = src.FirstName,
      LastName = src.LastName,
      BirthDate = src.BirthDate,
      BirthPlace = src.BirthPlace,
      City = src.City,
      UpdatedAtUtc = utcNow
    }
  };

  public static GetUserByIdResponse ToResponse(this User user) => new(
    UserId: user.Id,
    UserName: user.Credential.UserName,
    Email: user.Profile.Email,
    FirstName: user.Profile.FirstName,
    LastName: user.Profile.LastName,
    BirthDate: user.Profile.BirthDate,
    BirthPlace: user.Profile.BirthPlace,
    City: user.Profile.City,
    FullName: $"{user.Profile.FirstName} {user.Profile.LastName}"
  );
}
