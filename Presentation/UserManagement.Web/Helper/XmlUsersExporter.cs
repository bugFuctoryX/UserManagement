using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace UserManagement.Web.Helper;

public class XmlUsersExporter 
{
  public static string Serialize<T>(T data)
  {
    var serializer = new XmlSerializer(typeof(T));

    var settings = new XmlWriterSettings
    {
      Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false),
      Indent = true,
      OmitXmlDeclaration = false
    };

    using var sw = new Utf8StringWriter();
    using var xw = XmlWriter.Create(sw, settings);
    serializer.Serialize(xw, data);
    return sw.ToString();
  }

  private sealed class Utf8StringWriter : StringWriter
  {
    public override Encoding Encoding => Encoding.UTF8;
  }
}
