using MIS3.Trucks.Common.Domain.Entity;

namespace EdiPOC.Carriers;

public class Carrier : Entity
{
    public required Guid IdInDriver { get; init; }
    public required string Name { get; init; }
    public required string TerminalCode { get; init; }
    public List<Transport.Transport> Transports { get; init; } = [];
    public List<Cmr.Cmr> Cmrs { get; init; } = [];
    
    private Carrier() { }

    public static Carrier Create(Guid idInDriver, string terminalCode, string name, Guid? id = null)
    {
        return new Carrier()
        {
            TerminalCode = terminalCode,
            Name = name,
            IdInDriver = idInDriver,
            Id = id ?? Guid.NewGuid()
        };
    }
    
    internal bool IsCarrier(string name) => Name.Contains(name);
}

