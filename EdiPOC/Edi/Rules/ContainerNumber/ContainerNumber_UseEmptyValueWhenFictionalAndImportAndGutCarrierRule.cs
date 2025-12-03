using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.ContainerNumber;

internal sealed class ContainerNumber_UseEmptyValueWhenFictionalAndImportAndGutCarrierRule : EdiRule
{
    public override string Name => nameof(ContainerNumber_UseEmptyValueWhenFictionalAndImportAndGutCarrierRule);
    public override int Priority => 10;
    
    protected override bool IsApplicable(Transport.Transport transport) =>
        transport.IsImport() &&
        transport.IsContainerNumberFictional() &&
        transport.IsCarrierGut();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetContainerNumber(string.Empty);
        
        return edi;
    }
}