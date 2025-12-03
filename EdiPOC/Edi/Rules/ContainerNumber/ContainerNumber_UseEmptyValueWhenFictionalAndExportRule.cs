using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.ContainerNumber;

internal sealed class ContainerNumber_UseEmptyValueWhenFictionalAndExportRule : EdiRule
{
    public override string Name => nameof(ContainerNumber_UseEmptyValueWhenFictionalAndExportRule);
    public override int Priority => 10;
    
    protected override bool IsApplicable(Transport.Transport transport) =>
        transport.IsExport() && 
        transport.IsContainerNumberFictional();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetContainerNumber(string.Empty);
        
        return edi;
    }
}