using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.TransportPoint___TODO;

internal sealed class TODOTransportPointDateRule : EdiRule //TODO: MATO
{
    public override string Name => nameof(TODOTransportPointDateRule);
    public override int Priority => 1;

    protected override bool IsApplicable(Transport.Transport transport)
    {
        return false;
    }

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        throw new NotImplementedException(); // separatni storka
    }
}