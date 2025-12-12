using System.Text;
using System.Xml;

namespace EdiPOC.Edi.Domain.File;

internal sealed record XmlFile : File
{
    protected override string FilenamePostfix => ".xml";
    public override string ContentType => "application/xml";

    private XmlFile(MemoryStream xmlStream) : base(xmlStream)
    {
    }

    internal static XmlFile Create(XmlDocument xml)
    {
        xml.PreserveWhitespace = false;
        var settings = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "  ",
            NewLineHandling = NewLineHandling.Replace,
            NewLineChars = "\n",
            OmitXmlDeclaration = false,
            Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false)
        };
        using var xmlStream = new MemoryStream();
        using (var writer = XmlWriter.Create(xmlStream, settings))
        {
            xml.Save(writer);
        }

        return new XmlFile(xmlStream);
    }

    internal XmlDocument ToXmlDocument()
    {
        var xmlString = Encoding.UTF8.GetString(Content);
        var xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(xmlString);
        return xmlDocument;
    }
}