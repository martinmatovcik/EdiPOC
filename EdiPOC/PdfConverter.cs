using Mis.Common.Libraries.Pdf;

namespace EdiPOC;

public class PdfConverter(IPdfConverter pdfConverter)
{
    public async Task ConvertAsync(CancellationToken cancellationToken)
    {
        string html = HtmlEdiConverter.Convert();
        await using MemoryStream pdfStream = await pdfConverter.Convert(html, cancellationToken);
        string outputPath = "/Users/martinmatovcik/RiderProjects/EdiPOC/EdiPOC/edipdf.pdf";
        await File.WriteAllBytesAsync(outputPath, pdfStream.ToArray(), cancellationToken);
    }
}