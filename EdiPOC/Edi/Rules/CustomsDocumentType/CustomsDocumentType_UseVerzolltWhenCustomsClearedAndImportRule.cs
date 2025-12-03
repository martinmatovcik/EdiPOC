using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.CustomsDocumentType;

internal sealed class CustomsDocumentType_UseVerzolltWhenCustomsClearedAndImportRule : EdiRule
{
    public override string Name => nameof(CustomsDocumentType_UseVerzolltWhenCustomsClearedAndImportRule);
    public override int Priority => 10;
    private const string CustomsClearedText = "VERZOLLT";
    
    protected override bool IsApplicable(Transport.Transport transport) => transport.IsImport() && transport.CustomsDetails.IsCustomsCleared;

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetCustomsDocumentType(CustomsClearedText);

        return edi;
    }
}