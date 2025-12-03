using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.GateOut.Export;

internal sealed class GateOut_UseReleaseReferenceWhenExportRule : EdiRule
{
    public override string Name => nameof(GateOut_UseReleaseReferenceWhenExportRule);
    public override int Priority => 10;
    protected override bool IsApplicable(Transport.Transport transport) => transport.IsExport();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetGateOutReference(transport.OrderDetails.References.ReleaseReference ?? string.Empty);
        
        return edi;
    }
}