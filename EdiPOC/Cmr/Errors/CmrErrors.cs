using MIS3.Trucks.Common.Domain.Result;

namespace EdiPOC.Cmr.Errors;

public static class CmrErrors
{
    public static readonly Error NotFound = new(
        "Cmr.NotFound",
        "Cmr was not found by given identifier.",
        ErrorType.NOT_FOUND);
    
    public static Error CancellingInactive(Guid cmrId) 
        => new(
        "Cmr.CancellingInactive",
        $"Cmr with id {cmrId} is already inactive.",
        ErrorType.VALIDATION);
    
    public static Error UncancellingActive(Guid cmrId) 
        => new(
            "Cmr.UncancellingActive",
            $"Cmr with id {cmrId} is already active.",
            ErrorType.VALIDATION);
    
    public static Error AnotherCmrActive(Guid transportId) 
        => new(
            "Cmr.AnotherCmrActive",
            $"Transport with id {transportId} already has another active CMR, can't uncancel previous CMR.",
            ErrorType.VALIDATION);
}