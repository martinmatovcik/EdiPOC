using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.Note.SailSeal;

internal sealed class Note_AppendWhenSailSealNotRequestedAndExportRule : EdiRule
{
    public override string Name => nameof(Note_AppendWhenSailSealNotRequestedAndExportRule);
    public override int Priority => 10;
    private const string SailSealNotRequestedText = "Siegel aufhängen:NEIN";

    protected override bool IsApplicable(Transport.Transport transport) => transport.IsExport() && !transport.Services.IsSailSeal;

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
 
        edi.AppendNote(SailSealNotRequestedText);
        
        return edi;
    }
}