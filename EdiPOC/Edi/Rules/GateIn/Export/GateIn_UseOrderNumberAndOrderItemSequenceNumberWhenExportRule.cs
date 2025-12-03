using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.GateIn.Export;

internal sealed class GateIn_UseOrderNumberAndOrderItemSequenceNumberWhenExportRule : EdiRule
{
    public override string Name => nameof(GateIn_UseOrderNumberAndOrderItemSequenceNumberWhenExportRule);
    public override int Priority => 10;
    protected override bool IsApplicable(Transport.Transport transport) => transport.IsExport();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        edi.SetGateInReference($"{transport.OrderDetails.OrderNumber}-{transport.OrderDetails.OrderItemSequenceNumber}");

        return edi;
    }
}