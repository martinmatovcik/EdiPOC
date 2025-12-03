namespace EdiPOC.Transport;

/// <summary>
/// Details of the order in the opposite direction for roundtrip transports.
/// </summary>
/// <param name="OrderNumber">Order number from the order in the opposite direction.</param>
/// <param name="OrderItemSequenceNumber">Order item sequence number from the order in the opposite direction.</param>
public sealed record RoundtripDetails(string OrderNumber, int OrderItemSequenceNumber)
{
    /// <summary>
    /// Order number from order in opposite direction.
    /// </summary>
    public string OrderNumber { get; init; } = OrderNumber;
    
    /// <summary>
    /// Order item sequence number from order in opposite direction.
    /// </summary>
    public int OrderItemSequenceNumber { get; init; } = OrderItemSequenceNumber;
}