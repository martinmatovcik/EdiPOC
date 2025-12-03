namespace EdiPOC.Transport;

public sealed record References
{
    public string? ReleaseReference { get; init; }
    public string? TinReference { get; init; }
    public string? DepotReference { get; init; }
    public Mis3GateReference? GateInReference { get; init; }
    public Mis3GateReference? GateOutReference { get; init; }

    public References(string? releaseReference, string? tinReference, string? depotReference, Mis3GateReference? gateInReference, Mis3GateReference? gateOutReference)
    {
        ReleaseReference = releaseReference;
        TinReference = tinReference;
        DepotReference = depotReference;
        GateInReference = gateInReference;
        GateOutReference = gateOutReference;
    }

    private References() {} // Parameterless constructor for EF Core

    internal bool DepotReferenceExists() => DepotReference is not null;
    internal bool DepotReferenceContainsCharacter(char character) => DepotReferenceExists() && DepotReference!.Contains(character);
    internal bool IsGateInValid() => GateInReference is not null && GateInReference.IsValid;
    internal bool IsGateOutValid() => GateOutReference is not null && GateOutReference.IsValid;
}