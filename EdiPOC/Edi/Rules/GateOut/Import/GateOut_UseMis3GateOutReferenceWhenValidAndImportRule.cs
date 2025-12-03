using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.GateOut.Import;

internal sealed class GateOut_UseMis3GateOutReferenceWhenValidAndImportRule : EdiRule
{
    public override string Name => nameof(GateOut_UseMis3GateOutReferenceWhenValidAndImportRule);
    public override int Priority => 40;

    protected override bool IsApplicable(Transport.Transport transport) =>
        transport.IsImport() && transport.OrderDetails.References.IsGateOutValid();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetGateOutReference(transport.OrderDetails.References.GateOutReference!.GateReference);
        return edi;
    }
}