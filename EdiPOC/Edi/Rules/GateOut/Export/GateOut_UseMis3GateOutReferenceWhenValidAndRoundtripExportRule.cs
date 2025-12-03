using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.GateOut.Export;

internal sealed class GateOut_UseMis3GateOutReferenceWhenValidAndRoundtripExportRule : EdiRule
{
    public override string Name => nameof(GateOut_UseMis3GateOutReferenceWhenValidAndRoundtripExportRule);
    public override int Priority => 40;

    protected override bool IsApplicable(Transport.Transport transport) =>
        transport.IsExport() &&
        transport.OrderDetails.IsRoundTrip() &&
        transport.OrderDetails.References.IsGateOutValid();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetGateOutReference(transport.OrderDetails.References.GateOutReference!.GateReference);
        return edi;
    }
}