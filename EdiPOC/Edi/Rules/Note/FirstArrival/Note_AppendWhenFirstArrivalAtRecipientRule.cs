using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.Note.FirstArrival;

internal sealed class Note_AppendWhenFirstArrivalAtRecipientRule : EdiRule
{
    public override string Name => nameof(Note_AppendWhenFirstArrivalAtRecipientRule);
    public override int Priority => 10;
    private const string FirstArrivalAtRecipientText = "DIRECT ZUM EMPF.";

    protected override bool IsApplicable(Transport.Transport transport) => transport.LocationChain.IsFirstArrivalAtRecipient();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.AppendNote(FirstArrivalAtRecipientText);

        return edi;
    }
}