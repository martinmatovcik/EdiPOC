using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.GateIn.Import;

/// <summary>
/// Rule definition: https://metrans.atlassian.net/browse/TRUC-4104
/// </summary>
internal sealed class GateIn_UseDepotReferenceWithoutSlashWhenGutCarrierRule : EdiRule
{
    public override string Name => nameof(GateIn_UseDepotReferenceWithoutSlashWhenGutCarrierRule);
    public override int Priority => 50;
    
    private const char SplitCharacter = '/';

    protected override bool IsApplicable(Transport.Transport transport) =>
        transport.IsImport() &&
        transport.IsCarrierGut() &&
        !transport.OrderDetails.References.DepotReferenceContainsCharacter(SplitCharacter);

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetGateInReference(transport.OrderDetails.References.DepotReference!);
        
        return edi;
    }
}