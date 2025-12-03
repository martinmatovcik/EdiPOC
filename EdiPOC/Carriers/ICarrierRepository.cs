using MIS3.Trucks.Common.Domain.Entity;

namespace EdiPOC.Carriers;

public interface ICarrierRepository : IEntityRepository<Carrier>
{
    Task<Carrier?> GetByIdInDriverAsync(Guid carrierIdInDriver, CancellationToken ct);
}