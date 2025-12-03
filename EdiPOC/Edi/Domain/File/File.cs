using MIS3.Trucks.Common.Helpers;

namespace EdiPOC.Edi.Domain.File;

public abstract record File
{
    public string Filename { get; init; }
    protected abstract string FilenamePostfix { get; }
    public byte[] Content { get; }
    public abstract string ContentType { get; }

    private File(byte[] content)
    {
        Filename = CreateFilename();
        Content = content;
    }
    
    protected File(MemoryStream content) : this(content.ToArray())
    {
    }
    
    private string CreateFilename()
    {
        const string timestampFormat = "dd_MM_yyyy_HH_mm_ss";
        const int sequenceLength = 9;
        const string fileNamePrefix = "MET_";
        
        var timestamp = NodaTimeHelpers.GetFormattedTimestamp(Timezones.PragueTimezoneId, timestampFormat);
        var sequence = Guid.NewGuid().ToString("N")[..sequenceLength].ToUpperInvariant();

        return $"{fileNamePrefix}{timestamp}_{sequence}{FilenamePostfix}";
    }
}