using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.Note;

internal sealed class Note_AppendWhenExistsGoodWithAtaCodeRule : EdiRule
{
    public override string Name => nameof(Note_AppendWhenExistsGoodWithAtaCodeRule);
    public override int Priority => 10;

    protected override bool IsApplicable(Transport.Transport transport) => transport.GoodsDetails.ExistsGoodWithAtaCode();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        var ataCodes = transport.GoodsDetails.Goods
            .Where(x => !string.IsNullOrEmpty(x.AtaCode))
            .Select(x => x.AtaCode)
            .ToList();

        if (ataCodes.Count > 0)
            edi.AppendNote(string.Join(";", ataCodes));
        
        return edi;
    }
}