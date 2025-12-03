namespace EdiPOC.Transport;

public record WeightDetails
{
    public bool WeightingRequest { get; init; }
    public int? VgmWeight { get; init; }
    public int? TareWeight { get; init; }

    private WeightDetails(bool weightingRequest, int? vgmWeight, int? tareWeight)
    {
        WeightingRequest = weightingRequest;
        VgmWeight = vgmWeight;
        TareWeight = tareWeight;
    }

    public static WeightDetails Create(bool weightingRequest, int? vgmWeight, int? tareWeight)
    {
        return new WeightDetails(weightingRequest, vgmWeight, tareWeight);
    }

    internal bool IsWeighingRequested() => WeightingRequest;
}