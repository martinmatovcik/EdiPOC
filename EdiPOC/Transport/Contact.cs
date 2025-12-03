namespace EdiPOC.Transport;

public record Contact
{
    public string? Name { get; init; }
    public string? PhoneNumber { get; init; }
    public string? Email { get; init; }

    private Contact(string? name, string? phoneNumber, string? email)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    public static Contact Create(string? name, string? phoneNumber, string? email)
    {
        return new Contact(name, phoneNumber, email);
    }
}