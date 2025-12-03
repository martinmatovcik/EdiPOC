using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.GateIn.Import;

internal sealed class GateIn_UseContainerNumberWhenImportRule : EdiRule
{
    public override string Name => nameof(GateIn_UseContainerNumberWhenImportRule);
    public override int Priority => 10;
    protected override bool IsApplicable(Transport.Transport transport) => transport.IsImport();

    protected override Edi.Domain.Edi ApplyRuleFor(Edi.Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetGateInReference(transport.Container.ContainerNumber);
        
        return edi;
    }
}