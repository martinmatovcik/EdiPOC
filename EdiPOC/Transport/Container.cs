using EdiPOC.Codebook;
using Mis3.Trucks.Transport.De.Be.Api.Enum.Transport;
using NodaTime;

namespace EdiPOC.Transport;

public sealed record Container
{
    public string EnvelopeNumber { get; private set; } = "UNDEFINED_ENVELOPE_NUMBER";
    public string ContainerNumber { get; internal set; } = "UNDEFINED_CONTAINER_NUMBER";
    public string? OwnerCode { get; internal set; }
    public bool IsHeld { get; internal set; }
    public bool IsHeldByDepotWorker { get; internal set; }
    public LocalDateTime? TerminalArrival { get; internal set; }
    public WeightDetails WeightDetails { get; internal set; }
    public ContainerType ContainerType { get; internal set; } = new();
    public bool IsLoaded { get; init; }
    public string? TerminalPosition { get; internal set; }
    public List<string>? Seals { get; internal set; } = [];
    public ContainerTransportStatus TransportStatus { get; private set; } = ContainerTransportStatus.UNDEFINED;

    private Container() // Parameterless constructor for EF Core
    {
    }

    private Container(
        string envelopeNumber,
        string containerNumber,
        string? ownerCode,
        bool isHeld,
        bool isHeldByDepotWorker,
        LocalDateTime? terminalArrival,
        WeightDetails weightDetails,
        ContainerType containerType,
        bool isLoaded,
        string? terminalPosition,
        List<string> seals,
        ContainerTransportStatus transportStatus)
    {
        EnvelopeNumber = envelopeNumber;
        ContainerNumber = containerNumber;
        OwnerCode = ownerCode;
        IsHeld = isHeld;
        IsHeldByDepotWorker = isHeldByDepotWorker;
        TerminalArrival = terminalArrival;
        WeightDetails = weightDetails;
        ContainerType = containerType;
        IsLoaded = isLoaded;
        TerminalPosition = terminalPosition;
        Seals = seals;
        TransportStatus = transportStatus;
    }

    public static Container Create(string envelopeNumber, string containerNumber, string? ownerCode, bool isHeld, bool isHeldByDepotWorker, LocalDateTime? terminalArrival, WeightDetails weightDetails, ContainerType containerType, bool isLoaded, string? terminalPosition, List<string> seals, ContainerTransportStatus transportStatus)
    {
        return new Container(envelopeNumber, containerNumber, ownerCode, isHeld, isHeldByDepotWorker, terminalArrival, weightDetails, containerType, isLoaded, terminalPosition, seals, transportStatus);
    }

    internal bool SealExists() => Seals is not null && Seals.Count > 0;
}