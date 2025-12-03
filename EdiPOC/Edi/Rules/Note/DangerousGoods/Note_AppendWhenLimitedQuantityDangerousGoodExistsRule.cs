using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.Note.DangerousGoods;

internal sealed class Note_AppendWhenLimitedQuantityDangerousGoodExistsRule : EdiRule
{
    public override string Name => nameof(Note_AppendWhenLimitedQuantityDangerousGoodExistsRule);
    public override int Priority => 10;
    private const string DangerousGoodIsLimitedQuantityText = "Mindermenge von Gefargut - LQ";

    protected override bool IsApplicable(Transport.Transport transport) => transport.GoodsDetails.ExistsLimitedQuantityGood();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.AppendNote(DangerousGoodIsLimitedQuantityText);

        return edi;
    }
}