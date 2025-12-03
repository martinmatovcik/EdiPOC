using MIS3.Trucks.Common.Extensions;

namespace EdiPOC.Edi.Domain;

public record EdiContact(string Name, string PhoneNumber, string EMail)
{
    private readonly string _name = Name;
    private readonly string _phoneNumber = PhoneNumber;
    private readonly string _eMail = EMail;

    public string Name
    {
        get => _name;
        init => _name = value.RemoveControlCharacters() ?? string.Empty;
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        init => _phoneNumber = value.RemoveControlCharacters() ?? string.Empty;
    }

    public string EMail
    {
        get => _eMail;
        init => _eMail = value.RemoveControlCharacters() ?? string.Empty;
    }
}