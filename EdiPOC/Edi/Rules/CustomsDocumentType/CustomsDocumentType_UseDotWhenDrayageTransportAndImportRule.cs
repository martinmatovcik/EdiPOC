using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.CustomsDocumentType;

internal sealed class CustomsDocumentType_UseDotWhenDrayageTransportAndImportRule : EdiRule
{
    public override string Name => nameof(CustomsDocumentType_UseDotWhenDrayageTransportAndImportRule);
    public override int Priority => 20;
    private const string DrayageTransportText = ".";
    
    protected override bool IsApplicable(Transport.Transport transport) => transport.IsImport() && transport.IsDrayage;

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetCustomsDocumentType(DrayageTransportText);

        return edi;
    }
}