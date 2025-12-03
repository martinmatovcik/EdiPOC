using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.Note.UnloadingCode;

internal sealed class Note_AppendWhenUnloadingCodeExistsAndExportRule : EdiRule
{
    public override string Name => nameof(Note_AppendWhenUnloadingCodeExistsAndExportRule);
    public override int Priority => 10;
    private const string ReferenceCodePrefixText = "LadeRef:";

    protected override bool IsApplicable(Transport.Transport transport) => transport.IsExport() && transport.OrderDetails.UnloadingCodeExists();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.AppendNote($"{ReferenceCodePrefixText} {transport.OrderDetails.UnloadingCode}");

        return edi;
    }
}