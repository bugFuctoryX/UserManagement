namespace UserManagement.Application.Features.Users.Queries.ExportUsersXml;

public sealed record ExportFileResult(
    byte[] Content,
    string ContentType,
    string FileName
);