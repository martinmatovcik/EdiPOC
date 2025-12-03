namespace EdiPOC.Transport;

public sealed record OrderDetails
{
    public string OrderNumber { get; init; } = "UNDEFINED_ORDER_NUMBER";
    public Contact MetransContact { get; init; }
    public string? UnloadingCode { get; init; }
    public int OrderItemSequenceNumber { get; init; }
    public string? CostCenter { get; init; }
    public References References { get; init; }
    public RoundtripDetails? RoundtripDetails { get; init; }

    private OrderDetails() // Parameterless constructor for EF Core
    {
    }

    private OrderDetails(string orderNumber, Contact metransContact, string? unloadingCode, int orderItemSequenceNumber, string? costCenter, References references, RoundtripDetails? roundtripDetails)
    {
        OrderNumber = orderNumber;
        MetransContact = metransContact;
        UnloadingCode = unloadingCode;
        OrderItemSequenceNumber = orderItemSequenceNumber;
        CostCenter = costCenter;
        References = references;
        RoundtripDetails = roundtripDetails;       
    }

    public static OrderDetails Create(string orderNumber, Contact metransContact, string? unloadingCode, int orderItemSequenceNumber, string? costCenter, References references, RoundtripDetails? roundtripDetails) =>
        new(orderNumber, metransContact, unloadingCode, orderItemSequenceNumber, costCenter, references, roundtripDetails);
    
    internal bool UnloadingCodeExists() => !string.IsNullOrWhiteSpace(UnloadingCode);
    internal bool IsRoundTrip() => RoundtripDetails is not null;
    public bool IsGutGernsheimCostCenter() => CostCenter == "1485";
}