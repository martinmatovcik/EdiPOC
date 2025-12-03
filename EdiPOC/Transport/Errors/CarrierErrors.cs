using MIS3.Trucks.Common.Domain.Result;

namespace EdiPOC.Transport.Errors;

public static class CarrierErrors
{
    public const string CouldNotTranslateAddressToOsmIdCode = "Carrier.CouldNotTranslateAddressToOsmId";
    public const string MissingDeliveryDateCode = "Carrier.MissingDeliveryDate";
    public const string CouldNotGetValidCarriersCode = "Carrier.CouldNotGetValidCarriers";
    
    public static Error SameCarrierAssigned(Guid transportId)
    {
        return new Error("Error.SameCarrierAssigned", $"The carrier is already assigned to the same carrier for transport {transportId}", ErrorType.VALIDATION);
    }

    public static Error UnassignmentReasonMissing(Guid transportId)
    {
        return new Error("Error.UnassignmentReasonMissing", $"Transport {transportId} already has assigned carrier. Please provide unassignment reason for the current carrier", ErrorType.VALIDATION);
    }
    
    public static Error CarrierAssignedNotExist(Guid carrierId)
    {
        return new Error("Error.CarrierAssignedNotExist", $"Transport has carrier assigned but it doesn't exist in database in carriers table. Carrier Id: '{carrierId}'", ErrorType.NOT_FOUND);
    }
    
    public static Error CouldNotGetValidCarriers(Guid transportId)
    {
        return new Error(
            CouldNotGetValidCarriersCode,
            $"Could not get valid carriers for transport {transportId}",
            ErrorType.NOT_FOUND);
    }
    
    public static Error CouldNotTranslateAddressToOsmId(Guid transportId)
    {
        return new Error(
            CouldNotTranslateAddressToOsmIdCode,
            $"Number of osm ids returned is null. Transport id: {transportId}",
            ErrorType.NOT_FOUND);
    }

    public static Error DriverMicroserviceTimeout(Guid transportId)
    {
        return new Error(
            "Carrier.DriverMicroserviceTimeout",
            $"Request to get valid carriers for transport from driver microservice timed out. Transport id: {transportId}",
            ErrorType.FAILURE);
    }
    
    public static Error MissingDeliveryDate(Guid transportId)
    {
        return new Error(
            MissingDeliveryDateCode,
            $"Missing date of delivery for transport {transportId}",
            ErrorType.VALIDATION);
    }
}