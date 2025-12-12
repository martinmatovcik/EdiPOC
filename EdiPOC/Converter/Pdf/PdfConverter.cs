using System.Text;
using EdiPOC.Converter.Pdf.Html;
using EdiPOC.Edi.Domain.File;
using Medallion.Shell;
using File = System.IO.File;

namespace EdiPOC.Converter.Pdf;

internal class PdfConverter(IPdfConverter pdfConverter) : IEdiFileConverter
{
    public EdiFileFormat Format => EdiFileFormat.PDF;

    public async Task<Edi.Domain.File.File> ConvertAsync(Edi.Domain.Edi currentEdi, Edi.Domain.Edi? previousEdi, CancellationToken cancellationToken)
    {
        var data = new PdfEdiElements(currentEdi, previousEdi);
        var html = HtmlEdiConverter.Convert(data);
        
        string htmlOutput = "/Users/martinmatovcik/RiderProjects/EdiPOC/EdiPOC/ediHtml.html";
        await File.WriteAllBytesAsync(htmlOutput, Encoding.ASCII.GetBytes(html), CancellationToken.None);
        
        await using MemoryStream pdfStream = await pdfConverter.Convert(html, cancellationToken);
        string outputPath = "/Users/martinmatovcik/RiderProjects/EdiPOC/EdiPOC/edipdf.pdf";
        await File.WriteAllBytesAsync(outputPath, pdfStream.ToArray(), cancellationToken);

        return new PdfFile(pdfStream);
    }
}

public interface IPdfConverter
{
    Task<MemoryStream> Convert(string html, CancellationToken ct);
}

public class HtmlToPdfConverter : IPdfConverter
{
    public async Task<MemoryStream> Convert(string html, CancellationToken ct)
    {
        var stream = new MemoryStream();

        await Command.Run("weasyprint", ["-", "-"], o =>
            {
                o.CancellationToken(ct);
            })
            .RedirectFrom(html)
            .RedirectTo(stream)
            .Task;

        return stream;
    }
}