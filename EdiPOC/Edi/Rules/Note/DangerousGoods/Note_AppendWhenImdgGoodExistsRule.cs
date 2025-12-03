using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.Note.DangerousGoods;

internal sealed class Note_AppendWhenImdgGoodExistsRule : EdiRule
{
    public override string Name => nameof(Note_AppendWhenImdgGoodExistsRule);
    public override int Priority => 10;
    private const string DangerousGoodIsImdgText = "Kein Gefahrgut bei Beförderung auf der Straße gemäß ADR.Beförderung nach Absatz 1.1.4.2.1.";

    protected override bool IsApplicable(Transport.Transport transport) => transport.GoodsDetails.ExistsImdgGood();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.AppendNote(DangerousGoodIsImdgText);

        return edi;
    }
}