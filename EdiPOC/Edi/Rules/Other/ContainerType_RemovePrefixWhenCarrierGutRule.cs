using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.Other;

internal sealed class ContainerType_RemovePrefixWhenCarrierGutRule : EdiRule
{
    public override string Name => nameof(ContainerType_RemovePrefixWhenCarrierGutRule);
    public override int Priority => 10;

    protected override bool IsApplicable(Transport.Transport transport) => transport.IsCarrierGut();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.RemoveContainerTypePrefix();

        return edi;
    }
}