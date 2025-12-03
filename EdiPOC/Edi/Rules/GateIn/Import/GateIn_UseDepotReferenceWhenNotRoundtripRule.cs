using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.GateIn.Import;

/// <summary>
/// Rule definition: https://metrans.atlassian.net/browse/TRUC-4103
/// </summary>
internal sealed class GateIn_UseDepotReferenceWhenNotRoundtripRule : EdiRule
{
    public override string Name => nameof(GateIn_UseDepotReferenceWhenNotRoundtripRule);
    public override int Priority => 60;

    protected override bool IsApplicable(Transport.Transport transport) =>
        transport.IsImport() && transport.OrderDetails.References.DepotReferenceExists() && !transport.OrderDetails.IsRoundTrip();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetGateInReference(transport.OrderDetails.References.DepotReference!);
        
        return edi;
    }
}