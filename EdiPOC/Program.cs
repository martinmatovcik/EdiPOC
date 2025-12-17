using EdiPOC.Converter.Pdf;
using EdiPOC.Data;
using EdiPOC.Edi.Domain;

Console.WriteLine("Starting...");

// await CreateNewPdf();
await CreateChangePdf();

Console.WriteLine("Done!");
return;

async Task CreateChangePdf()
{
    PdfConverter pdfConverter = new PdfConverter(new HtmlToPdfConverter());
    var previousEdi = EdiData.CreateEdiWithRealData(EdiActionType.NEW,5, "CMR-1");
    var currentEdi = EdiData.CreateEdiWithRealData(EdiActionType.CHANGE, 6, "CMR-2");

    await pdfConverter.ConvertAsync(previousEdi, currentEdi, CancellationToken.None);
}

async Task CreateNewPdf()
{
    PdfConverter pdfConverter = new PdfConverter(new HtmlToPdfConverter());
    Edi? previousEdi = null;
    Edi currentEdi = EdiData.CreateEdiWithRealData(EdiActionType.NEW, 5);

    await pdfConverter.ConvertAsync(previousEdi, currentEdi, CancellationToken.None);
}