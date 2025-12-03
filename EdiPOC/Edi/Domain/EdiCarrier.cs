using MIS3.Trucks.Common.Extensions;

namespace EdiPOC.Edi.Domain;

public sealed record EdiCarrier(string Name, string Street, string City, string PostalCode, string Country)
{
    private readonly string _name = Name;
    private readonly string _street = Street;
    private readonly string _postalCode = PostalCode;
    private readonly string _country = Country;
    private readonly string _city = City;

    public string Name
    {
        get => _name;
        init => _name = value.RemoveControlCharacters() ?? string.Empty;
    }

    public string Street
    {
        get => _street;
        init => _street = value.RemoveControlCharacters() ?? string.Empty;
    }

    public string PostalCode
    {
        get => _postalCode;
        init => _postalCode = value.RemoveControlCharacters() ?? string.Empty;
    }

    public string Country
    {
        get => _country;
        init => _country = value.RemoveControlCharacters() ?? string.Empty;
    }

    public string City
    {
        get => _city;
        init => _city = value.RemoveControlCharacters() ?? string.Empty;
    }
}