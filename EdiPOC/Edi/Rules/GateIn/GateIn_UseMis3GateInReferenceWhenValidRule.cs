using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.GateIn;

internal sealed class GateIn_UseMis3GateInReferenceWhenValidRule : EdiRule
{
    public override string Name => nameof(GateIn_UseMis3GateInReferenceWhenValidRule);
    public override int Priority => 40;

    protected override bool IsApplicable(Transport.Transport transport) => transport.OrderDetails.References.IsGateInValid();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetGateInReference(transport.OrderDetails.References.GateInReference!.GateReference);
        
        return edi;
    }
}