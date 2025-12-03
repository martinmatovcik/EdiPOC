using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.CustomerReference;

internal sealed class CustomerReference_UseEnvelopeWhenDussTerminalAndMis3OrderRule : EdiRule
{
    public override string Name => nameof(CustomerReference_UseEnvelopeWhenDussTerminalAndMis3OrderRule);
    public override int Priority => 10;

    protected override bool IsApplicable(Transport.Transport transport) => 
        !transport.IsMis2Origin() && transport.IsDussTerminal();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetReferenceNumber(transport.Container.EnvelopeNumber);

        return edi;
    }
}