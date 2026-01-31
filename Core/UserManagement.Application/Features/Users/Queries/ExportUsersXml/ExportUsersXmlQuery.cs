namespace UserManagement.Application.Features.Users.Queries.ExportUsersXml;

public sealed record ExportUsersXmlQuery(IReadOnlyList<UserExportRow> Rows)
    : IRequest<Result<ExportFileResult>>;

public sealed record UserExportRow(
    Guid UserId,
    string UserName,
    string Email,
    DateOnly BirthDate,
    string BirthPlace,
    string City,
    string FullName
);