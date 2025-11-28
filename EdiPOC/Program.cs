// See https://aka.ms/new-console-template for more information

using EdiPOC;

Console.WriteLine("Starting...");
var pdfConverter = new PdfConverter(new HtmlToPdfConverter());
await pdfConverter.ConvertAsync(CancellationToken.None);
Console.WriteLine("Done!");