using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.Other;

/// <summary>
/// Sets the <c>IsImport</c> attribute to 2 for transports handled by the "EKB" carrier.
/// </summary>
/// <remarks>
/// This rule has a priority of 1.
/// </remarks>
internal sealed class ImportExportValue_Fill2WhenCarrierEkbRule : EdiRule
{
    public override string Name => nameof(ImportExportValue_Fill2WhenCarrierEkbRule);
    public override int Priority => 10;
    protected override bool IsApplicable(Transport.Transport transport) => transport.IsCarrierEkb();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetImportExportValue(2);
        
        return edi;
    }
}