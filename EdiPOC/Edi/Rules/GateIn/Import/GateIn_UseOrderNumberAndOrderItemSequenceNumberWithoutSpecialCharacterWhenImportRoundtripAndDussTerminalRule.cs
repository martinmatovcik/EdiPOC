using EdiPOC.Edi.Rules.Abstraction;

namespace EdiPOC.Edi.Rules.GateIn.Import;

internal sealed class GateIn_UseOrderNumberAndOrderItemSequenceNumberWithoutSpecialCharacterWhenImportRoundtripAndDussTerminalRule : EdiRule
{
    public override string Name => nameof(GateIn_UseOrderNumberAndOrderItemSequenceNumberWithoutSpecialCharacterWhenImportRoundtripAndDussTerminalRule);
    public override int Priority => 30;

    protected override bool IsApplicable(Transport.Transport transport) =>
        transport.IsImport() && transport.OrderDetails.IsRoundTrip() && transport.IsDussTerminal();

    protected override Domain.Edi ApplyRuleFor(Domain.Edi edi, Transport.Transport transport)
    {
        var roundtripDetails = transport.OrderDetails.RoundtripDetails!;
        edi.SetGateInReference($"{roundtripDetails.OrderNumber}{roundtripDetails.OrderItemSequenceNumber}");
        
        return edi;
    }
}