namespace EdiPOC.Carriers;

public record CarrierLocation(
    string? Street,
    string? HouseNumber,
    string? City,
    string? PostalCode,
    string? CountryIso);