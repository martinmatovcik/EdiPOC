using MIS3.Trucks.Common.Domain.Result;

namespace EdiPOC.Transport.Errors;

public static class CommonErrors
{
    public static Error JwtSurnameMissing()
    {
        return new Error("Error.JwtSurnameMissing", "Authorization header is missing surname claim", ErrorType.VALIDATION);
    }
}