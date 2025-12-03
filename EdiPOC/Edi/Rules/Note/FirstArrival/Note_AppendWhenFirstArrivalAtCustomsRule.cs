using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.Note.FirstArrival;

internal sealed class Note_AppendWhenFirstArrivalAtCustomsRule : EdiRule
{
    public override string Name => nameof(Note_AppendWhenFirstArrivalAtCustomsRule);
    public override int Priority => 10;
    private const string FirstArrivalAtCustomsText = "ZUERST AUF ZOLLAMT";

    protected override bool IsApplicable(Transport.Transport transport) => transport.LocationChain.IsFirstArrivalAtCustoms();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.AppendNote(FirstArrivalAtCustomsText);

        return edi;
    }
}