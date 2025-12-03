using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.WeighingRequest;

internal sealed class IsWeighingRequest_FillWhenRequestedAndExportRule : EdiRule
{
    public override string Name => nameof(IsWeighingRequest_FillWhenRequestedAndExportRule);
    public override int Priority => 10;
    private const string WeighingRequestedText = "JA";
    
    protected override bool IsApplicable(Transport.Transport transport) => transport.IsExport() && transport.Container.WeightDetails.IsWeighingRequested();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetWeighingRequest(WeighingRequestedText);
                    
        return edi;
    }
}