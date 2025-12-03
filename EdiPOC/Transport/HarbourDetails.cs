namespace EdiPOC.Transport;

public record HarbourDetails
{
    private HarbourDetails() // Parameterless constructor for EF Core
    {
    }
    
    public string? DestinationHarbour { get; init; }
    public string? DestinationHarbourCountryCode { get; init; }
    public string? HarbourCode { get; init; }
    
    private HarbourDetails(string? destinationHarbour, string? destinationHarbourCountryCode, string? harbourCode)
    {
        DestinationHarbour = destinationHarbour;
        DestinationHarbourCountryCode = destinationHarbourCountryCode;
        HarbourCode = harbourCode;
    }
    
    public static HarbourDetails Create(string? destinationHarbour, string? destinationHarbourCountryCode, string? harbourCode) => new(destinationHarbour, destinationHarbourCountryCode, harbourCode);
}