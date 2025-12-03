using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.WeighingRequest;

internal sealed class IsWeighingRequest_FillWhenNotRequestedAndExportRule : EdiRule
{
    public override string Name => nameof(IsWeighingRequest_FillWhenNotRequestedAndExportRule);
    public override int Priority => 10;
    private const string WeighingNotRequestedText = "NEIN";
    
    protected override bool IsApplicable(Transport.Transport transport) => transport.IsExport() && !transport.Container.WeightDetails.IsWeighingRequested();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetWeighingRequest(WeighingNotRequestedText);
                    
        return edi;
    }
}