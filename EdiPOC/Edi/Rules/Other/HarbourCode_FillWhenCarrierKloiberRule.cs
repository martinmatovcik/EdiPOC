using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.Other;

internal sealed class HarbourCode_FillWhenCarrierKloiberRule : EdiRule
{
    public override string Name => nameof(HarbourCode_FillWhenCarrierKloiberRule);
    public override int Priority => 10;
    
    protected override bool IsApplicable(Transport.Transport transport) => transport.IsCarrierKloiber();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetHarbourCode(transport.HarbourDetails.HarbourCode);

        return edi;
    }
}