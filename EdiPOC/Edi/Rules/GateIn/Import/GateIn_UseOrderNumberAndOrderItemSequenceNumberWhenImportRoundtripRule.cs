using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.GateIn.Import;

internal sealed class GateIn_UseOrderNumberAndOrderItemSequenceNumberWhenImportRoundtripRule : EdiRule
{
    public override string Name => nameof(GateIn_UseOrderNumberAndOrderItemSequenceNumberWhenImportRoundtripRule);
    public override int Priority => 20;
    protected override bool IsApplicable(Transport.Transport transport) =>
        transport.IsImport() && transport.OrderDetails.IsRoundTrip();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        var roundtripDetails = transport.OrderDetails.RoundtripDetails!;
        edi.SetGateInReference($"{roundtripDetails.OrderNumber}-{roundtripDetails.OrderItemSequenceNumber}");
        
        return edi;
    }
}