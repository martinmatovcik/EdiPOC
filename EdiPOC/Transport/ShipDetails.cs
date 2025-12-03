namespace EdiPOC.Transport;

public record ShipDetails
{
    public string? ShipName { get; private set; }
    public bool IsImportFromShip { get; private set; }
    public string? ShippingCompany { get; init; }
    
    internal bool IsUnknownShippingCompany() => string.IsNullOrWhiteSpace(ShippingCompany);

    private ShipDetails(string? shipName, bool isImportFromShip, string? shippingCompany)
    {
        ShipName = shipName;
        IsImportFromShip = isImportFromShip;
        ShippingCompany = shippingCompany;
    }

    public static ShipDetails Create(string? shipName, bool isImportFromShip, string? shippingCompany) =>
        new(shipName, isImportFromShip, shippingCompany);

};