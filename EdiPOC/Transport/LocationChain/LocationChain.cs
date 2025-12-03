using MIS3.Trucks.Common.Extensions;
using Mis3.Trucks.Transport.De.Be.Api.Enum.Transport;

namespace EdiPOC.Transport.LocationChain;

public sealed record LocationChain
{
    public LocationItem? First { get; private set; }
    public List<LocationItem> ImportExport { get; private set; } = [];
    public LocationItem? Customs { get; private set; }
    public LocationItem? Declaration { get; private set; }
    public LocationItem? Last { get; private set; }
    public LocationItem? Consignee { get; internal set; }
    public FirstArrival FirstArrival { get; private set; } = FirstArrival.SEE_SCHEDULE;
    public string? TerminalFrom { get; private set; }
    public string? TerminalTo { get; private set; }

    private LocationChain() {} // Parameterless constructor for EF Core

    private LocationChain(
        LocationItem? first,
        List<LocationItem> importExport,
        LocationItem? customs,
        LocationItem? declaration,
        LocationItem? last,
        LocationItem? consignee,
        FirstArrival firstArrival,
        string? terminalFrom,
        string? terminalTo)
    {
        First = first;
        ImportExport = importExport;
        Customs = customs;
        Declaration = declaration;
        Last = last;
        Consignee = consignee;
        FirstArrival = firstArrival;
        TerminalFrom = terminalFrom;
        TerminalTo = terminalTo;
    }

    public static LocationChain Create(
        LocationItem? first,
        List<LocationItem>? importExport,
        LocationItem? customs,
        LocationItem? declaration,
        LocationItem? last,
        LocationItem? consignee,
        FirstArrival firstArrival,
        string? terminalFrom,
        string? terminalTo) =>
        new(first, importExport ?? [], customs, declaration, last, consignee, firstArrival, terminalFrom, terminalTo);

    public LocationItem GetFirstImportExportLocation()
    {
        if (ImportExport.IsNullOrEmpty())
            throw new InvalidOperationException("Trying to get first import/export location, but list is empty.");

        return ImportExport.OrderBy(x => x.ChainSequence).First();
    }
    
    internal bool IsFirstArrivalAtCustoms() => FirstArrival == FirstArrival.CUSTOMS;
    
    internal bool IsFirstArrivalAtRecipient() => FirstArrival == FirstArrival.RECIPIENT;
}