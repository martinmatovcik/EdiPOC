using MIS3.Trucks.Common.Domain.Result;

namespace EdiPOC.Transport.Errors;

public static class TransportErrors
{
    public static readonly Error NotFound = new(
        "Transport.NotFound",
        "Transport was not found by given identifier.",
        ErrorType.NOT_FOUND);
    
    public static readonly Error HasActiveCmr = new(
        "Transport.HasActiveCmr",
        "Transport already has active cmr.",
        ErrorType.VALIDATION);
    
    public static readonly Error NotAssignedToCarrier = new(
        "Transport.NotAssignedToCarrier",
        "Transport is not assigned to carrier.",
        ErrorType.VALIDATION);
    
    public static readonly Error IsNotCreated = new(
        "Transport.IsNotCreated",
        "Transport is not in status created.",
        ErrorType.VALIDATION);
    
    public static Error TransportCarrierMissing(Guid transportId)
    {
        return new Error("Error.TransportCarrierMissing", $"Transport with id {transportId} does not have carrier assigned", ErrorType.VALIDATION);
    }

    public static Error AnotherActiveCmr(Guid cmrId, Guid transportId)
    {
        return new Error("Error.AnotherActiveCmr",$"Trying to cancel CMR with ID {cmrId} for transport with ID {transportId} but there is another active CMR", ErrorType.VALIDATION);
    }
}