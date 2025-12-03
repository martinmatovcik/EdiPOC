using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.Other;

internal sealed class ReleaseReference_FillWhenExportRule : EdiRule
{
    public override string Name => nameof(ReleaseReference_FillWhenExportRule);
    public override int Priority => 10;
    
    protected override bool IsApplicable(Transport.Transport transport) => transport.IsExport();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetReleaseReference(transport.OrderDetails.References.ReleaseReference);

        return edi;
    }
}