using File = EdiPOC.Edi.Domain.File.File;

namespace EdiPOC.Converter;

public interface IEdiFileConverter
{
    EdiFileFormat Format { get; }
    
    Task<File> ConvertAsync(Edi.Domain.Edi? previous, Edi.Domain.Edi current, CancellationToken cancellationToken);
}