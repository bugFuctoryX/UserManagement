namespace UserManagement.Application.Features.Users.Queries.ExportUsersXml;

public sealed class ExportUsersXmlHandler : IRequestHandler<ExportUsersXmlQuery, Result<ExportFileResult>>
{
  public Task<Result<ExportFileResult>> Handle(ExportUsersXmlQuery request, CancellationToken ct)
  {
    if (request.Rows is null || request.Rows.Count == 0)
      return Task.FromResult(Result<ExportFileResult>.Fail(ErrorType.BadRequest, "No rows to export."));

    var payload = request.Rows.ToXml();
    var bytes = XmlUsersExporter.SerializeToUtf8Bytes(payload);
    var fileName = $"users_{DateTime.UtcNow:yyyyMMdd_HHmmss}.xml";

    return Task.FromResult(Result<ExportFileResult>.Ok(new ExportFileResult(bytes, "application/xml", fileName)));
  }

}