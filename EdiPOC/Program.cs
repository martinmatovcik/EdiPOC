// See https://aka.ms/new-console-template for more information

using EdiPOC;

Console.WriteLine("Starting...");
var pdfConverter = new PdfConverter(new HtmlToPdfConverter());

var edi = EdiData.CreateEdiWithRealData();

await pdfConverter.ConvertAsync(edi, CancellationToken.None);
Console.WriteLine("Done!");