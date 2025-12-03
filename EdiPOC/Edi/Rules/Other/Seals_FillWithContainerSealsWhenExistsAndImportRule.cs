using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.Other;

internal sealed class Seals_FillWithContainerSealsWhenExistsAndImportRule : EdiRule
{
    public override string Name => nameof(Seals_FillWithContainerSealsWhenExistsAndImportRule);
    public override int Priority => 10;
    
    protected override bool IsApplicable(Transport.Transport transport) => transport.IsImport() && transport.Container.SealExists();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetSeals(transport.Container.Seals!);
        
        return edi;
    }
}