using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.Note.UnloadingCode;

internal sealed class Note_AppendWhenUnloadingCodeExistsAndImportRule : EdiRule
{
    public override string Name => nameof(Note_AppendWhenUnloadingCodeExistsAndImportRule);
    public override int Priority => 10;
    private const string ReferenceCodePrefixText = "EmpfCode:";

    protected override bool IsApplicable(Transport.Transport transport) => transport.IsImport() && transport.OrderDetails.UnloadingCodeExists();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.AppendNote($"{ReferenceCodePrefixText} {transport.OrderDetails.UnloadingCode}");

        return edi;
    }
}