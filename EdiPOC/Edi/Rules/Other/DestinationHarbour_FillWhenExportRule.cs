using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.Other;

internal sealed class DestinationHarbour_FillWhenExportRule : EdiRule
{
    public override string Name => nameof(DestinationHarbour_FillWhenExportRule);
    public override int Priority => 10;
    
    protected override bool IsApplicable(Transport.Transport transport) => transport.IsExport();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetDestinationHarbour(transport.HarbourDetails.DestinationHarbour, transport.HarbourDetails.DestinationHarbourCountryCode);
        
        return edi;
    }
}