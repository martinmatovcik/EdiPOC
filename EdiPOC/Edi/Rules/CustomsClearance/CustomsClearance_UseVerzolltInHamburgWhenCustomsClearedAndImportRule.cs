using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.CustomsClearance;

internal sealed class CustomsClearance_UseVerzolltInHamburgWhenCustomsClearedAndImportRule : EdiRule
{
    public override string Name => nameof(CustomsClearance_UseVerzolltInHamburgWhenCustomsClearedAndImportRule);
    public override int Priority => 10;
    private const string CustomsClearedInHamburgText = "VERZOLLT IN HAMBURG";
    
    protected override bool IsApplicable(Transport.Transport transport) => transport.IsImport() && transport.CustomsDetails.IsCustomsCleared;

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
   {
       edi.SetCustomsClearance(CustomsClearedInHamburgText);
        
        return edi;
    }
}