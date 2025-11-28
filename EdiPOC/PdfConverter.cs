using Medallion.Shell;

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