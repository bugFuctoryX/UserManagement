namespace UserManagement.Application.Features.Users.Queries.ExportUsersXml.Xml;

public static class XmlUsersExporter
{
  public static byte[] SerializeToUtf8Bytes<T>(T data)
  {
    var serializer = new XmlSerializer(typeof(T));

    var settings = new XmlWriterSettings
    {
      Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false),
      Indent = true,
      OmitXmlDeclaration = false
    };

    using var ms = new MemoryStream();
    using (var xw = XmlWriter.Create(ms, settings))
    {
      serializer.Serialize(xw, data);
    }

    return ms.ToArray();
  }
}
