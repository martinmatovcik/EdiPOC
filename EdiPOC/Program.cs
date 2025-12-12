// See https://aka.ms/new-console-template for more information

using EdiPOC;
using EdiPOC.Converter.Pdf;
using EdiPOC.Data;

Console.WriteLine("Starting...");
var pdfConverter = new PdfConverter(new HtmlToPdfConverter());

var previousEdi = EdiData.CreateEdiWithRealData(4, "CMR-1");
var currentEdi = EdiData.CreateEdiWithRealData(4, "CMR-2");

await pdfConverter.ConvertAsync(previousEdi, currentEdi, CancellationToken.None);
Console.WriteLine("Done!");