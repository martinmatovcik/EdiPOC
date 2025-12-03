using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.CustomsClearance;

internal sealed class CustomsClearance_UseDotWhenDrayageTransportAndImportRule : EdiRule
{
    public override string Name => nameof(CustomsClearance_UseDotWhenDrayageTransportAndImportRule);
    public override int Priority => 20;
    
    protected override bool IsApplicable(Transport.Transport transport) => transport.IsImport() && transport.IsDrayage;

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetCustomsClearance(".");
        
        return edi;
    }
}