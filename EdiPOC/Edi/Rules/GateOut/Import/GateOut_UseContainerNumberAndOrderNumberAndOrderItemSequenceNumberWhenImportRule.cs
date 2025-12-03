using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.GateOut.Import;

internal sealed class GateOut_UseContainerNumberAndOrderNumberAndOrderItemSequenceNumberWhenImportRule : EdiRule
{
    public override string Name => nameof(GateOut_UseContainerNumberAndOrderNumberAndOrderItemSequenceNumberWhenImportRule);
    public override int Priority => 10;
    protected override bool IsApplicable(Transport.Transport transport) => transport.IsImport();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetGateOutReference($"{transport.Container.ContainerNumber}/{transport.OrderDetails.OrderNumber}-{transport.OrderDetails.OrderItemSequenceNumber}");
        
        return edi;
    }
}