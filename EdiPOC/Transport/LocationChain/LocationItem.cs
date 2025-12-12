namespace EdiPOC.Transport.LocationChain;

public sealed record LocationItem
{
    public int? ChainSequence { get; init; }
    public string Name { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string Street { get; init; } = string.Empty;
    public string PostalCode { get; init; } = string.Empty;
    public string CountryIso { get; init; } = string.Empty;
    public Contact? Contact { get; init; }
    public LocationType LocationType { get; init; } = LocationType.UNDEFINED;
    
    private LocationItem() // Parameterless constructor for EF Core
    {
    }

    private LocationItem(int? chainSequence, string name, string city, string street, string postalCode, string countryIso, Contact? contact, LocationType locationType)
    {
        ChainSequence = chainSequence;
        Name = name;
        City = city;
        Street = street;
        PostalCode = postalCode;
        CountryIso = countryIso;
        Contact = contact;
        LocationType = locationType;
    }

    public static LocationItem Create(int? chainSequence, string? name, string? city, string? street, string? postalCode, string? countryIso, Contact? contact, LocationType locationType) =>
        new(chainSequence, name ?? string.Empty, city ?? string.Empty, street ?? string.Empty, postalCode ?? string.Empty, countryIso ?? string.Empty, contact, locationType);
    
    internal string BuildAddress()
    {
        return $"{Street} {PostalCode} {City} {CountryIso}";
    }
} 