using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.Note;

internal sealed class Note_AppendWhenContainerIsWasteRule : EdiRule
{
    public override string Name => nameof(Note_AppendWhenContainerIsWasteRule);
    public override int Priority => 10;
    private const string IsWasteText = "(ABFALL)";

    protected override bool IsApplicable(Transport.Transport transport) => transport.Services.IsWaste;

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.AppendNote(IsWasteText);

        return edi;
    }
}