using System.Collections.Immutable;
using System.Text;

namespace EdiPOC.Converter.Pdf.Html;

internal static partial class HtmlEdiConverter
{
    public static string Convert(PdfEdiElements data)
    {
        var builder = new StringBuilder();
        builder.AppendHead().AppendBody(data).EndHtml();
        return builder.ToString();
    }

    private static string InsertValue(IImmutableDictionary<string, PdfEdiElements.Data> bodyData, string dataKey) =>
        InsertValue(GetDataForBody(bodyData, dataKey));

    private static string InsertValue(PdfEdiElements.Data data) => data.Value ?? string.Empty;

    private static string MarkHighlighted(IImmutableDictionary<string, PdfEdiElements.Data> bodyData, string dataKey) =>
        MarkHighlighted(GetDataForBody(bodyData, dataKey));

    private static string MarkHighlighted(PdfEdiElements.Data data) => MarkHighlighted(data.IsHighlighted);

    private static string MarkHighlighted(PdfEdiElements.StopData data) => MarkHighlighted(data.IsHighlighted);

    private static string MarkHighlighted(bool isHighlighted)
    {
        const string highlighted = " class =\"highlighted\""; //space at the start is mandatory
        return isHighlighted ? highlighted : string.Empty;
    }

    private static PdfEdiElements.Data GetDataForBody(IImmutableDictionary<string, PdfEdiElements.Data> bodyData,
        string dataKey)
    {
        bodyData.TryGetValue(dataKey, out var data);
        if (data is null)
            throw new KeyNotFoundException($"Key {dataKey} not found in body data for Edi.PDF generation");

        return data;
    }

    private static void EndHtml(this StringBuilder builder) => builder.AppendLine("</html>");
}