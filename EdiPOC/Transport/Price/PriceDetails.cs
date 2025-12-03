using Mis3.Trucks.Transport.De.Be.Api.Events.Private.Commands.Dto.Price;

namespace EdiPOC.Transport.Price;

public record PriceDetails(int? Price, CurrencyCode Currency, int? DistanceInKm, int Zone = 0)
{
    public int Zone { get; set; } = Zone;
    
    public static PriceDetails CreateEur(int? price, int? distanceInKm)
    {
        return new PriceDetails(price, CurrencyCode.EUR, distanceInKm);
    }
}