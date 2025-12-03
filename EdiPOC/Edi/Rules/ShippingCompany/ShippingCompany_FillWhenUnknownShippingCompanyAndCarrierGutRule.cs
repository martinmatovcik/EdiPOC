using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.ShippingCompany;

internal sealed class ShippingCompany_FillWhenUnknownShippingCompanyAndCarrierGutRule : EdiRule
{
    public override string Name => nameof(ShippingCompany_FillWhenUnknownShippingCompanyAndCarrierGutRule);
    public override int Priority => 10;
    private const string UnknownText = "UNKNOWN";
    
    protected override bool IsApplicable(Transport.Transport transport) => transport.IsCarrierGut() && transport.ShipDetails.IsUnknownShippingCompany();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetShippingCompany(UnknownText);
        
        return edi;
    }
}