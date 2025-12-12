using MIS3.Trucks.Common.Extensions;

namespace EdiPOC.Edi.Domain;

public sealed record EdiLocation(
    EdiLocationType Type,
    int SequenceNumber,
    string Company,
    string CountryIso,
    string PostalCode,
    string City,
    string Street,
    string? GateInReference,
    string? DeliveryText,
    string? GateOutReference)
{
    private string _company = Company;
    private string _countryIso = CountryIso;
    private string _postalCode = PostalCode;
    private string _city = City;
    private string _street = Street;
    private string? _gateInReference = GateInReference;
    private string? _deliveryText = DeliveryText;
    private string? _gateOutReference = GateOutReference;

    public EdiLocationType Type { get; private set; } = Type;

    public int SequenceNumber { get; private set; } = SequenceNumber;

    public string Company
    {
        get => _company;
        private set => _company = value.RemoveControlCharacters() ?? string.Empty;
    }

    public string CountryIso
    {
        get => _countryIso;
        private set => _countryIso = value.RemoveControlCharacters() ?? string.Empty;
    }

    public string PostalCode
    {
        get => _postalCode;
        private set => _postalCode = value.RemoveControlCharacters() ?? string.Empty;
    }

    public string City
    {
        get => _city;
        private set => _city = value.RemoveControlCharacters() ?? string.Empty;
    }

    public string Street
    {
        get => _street;
        private set => _street = value.RemoveControlCharacters() ?? string.Empty;
    }

    public string? GateInReference
    {
        get => _gateInReference;
        internal set => _gateInReference = value.RemoveControlCharacters();
    }

    public string? DeliveryText
    {
        get => _deliveryText;
        private set => _deliveryText = value.RemoveControlCharacters();
    }

    public string? GateOutReference
    {
        get => _gateOutReference;
        internal set => _gateOutReference = value.RemoveControlCharacters();
    }

    internal bool IsPickupLocation() => Type == EdiLocationType.Pickup;
    internal bool IsDropoffLocation() => Type == EdiLocationType.Dropoff;
}