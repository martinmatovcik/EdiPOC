using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.Other;

internal sealed class ShipName_RemoveWhenNotImportedFromShipAndImportRule : EdiRule
{
    public override string Name => nameof(ShipName_RemoveWhenNotImportedFromShipAndImportRule);
    public override int Priority => 10;
    
    protected override bool IsApplicable(Transport.Transport transport) => transport.IsImport() && !transport.ShipDetails.IsImportFromShip;

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetShipName(null);
        
        return edi;
    }
}