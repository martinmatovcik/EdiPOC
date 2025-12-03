using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.Note.SailSeal;

internal sealed class Note_AppendWhenSailSealRequestedAndExportRule : EdiRule
{
    public override string Name => nameof(Note_AppendWhenSailSealRequestedAndExportRule);
    public override int Priority => 10;
    private const string SailSealRequestedText = "Siegel aufhängen:JA";


    protected override bool IsApplicable(Transport.Transport transport) => transport.IsExport() && transport.Services.IsSailSeal;

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.AppendNote(SailSealRequestedText);
        
        return edi;
    }
}