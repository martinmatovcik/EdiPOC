using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.GateIn.Import;

/// <summary>
/// Rule definition: https://metrans.atlassian.net/browse/TRUC-4102
/// </summary>
internal sealed class GateIn_UseTinReferenceWhenShippingCompanyOrderAndGutGernsheimCostCentreRule : EdiRule
{
    public override string Name => nameof(GateIn_UseTinReferenceWhenShippingCompanyOrderAndGutGernsheimCostCentreRule);
    public override int Priority => 70;

    protected override bool IsApplicable(Transport.Transport transport) =>
        transport.IsImport() &&
        transport.OrderDetails.References.TinReference is not null &&
        transport.IsShippingCompanyOrder() &&
        transport.OrderDetails.IsGutGernsheimCostCenter();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetGateInReference(transport.OrderDetails.References.TinReference!);
        
        return edi;
    }
}