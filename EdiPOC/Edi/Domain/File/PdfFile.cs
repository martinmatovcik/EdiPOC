namespace EdiPOC.Edi.Domain.File;

internal sealed record PdfFile : File
{
    protected override string FilenamePostfix => ".pdf";
    public override string ContentType => "application/pdf";

    internal PdfFile(MemoryStream pdfStream) : base(pdfStream)
    {
    }
}
