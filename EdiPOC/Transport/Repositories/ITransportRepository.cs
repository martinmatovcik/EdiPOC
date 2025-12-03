using EdiPOC.Transport.History;
using MIS3.Trucks.Common.Domain.Entity;
using Mis3.Trucks.Transport.De.Be.Api.Features.Transport.Dto;
using NodaTime;

namespace EdiPOC.Transport.Repositories;

public interface ITransportRepository : IEntityRepository<Transport>
{
    ///  <summary>
    /// Gets a queryable collection of Transport entities.
    /// </summary>
    IQueryable<Transport> GetQueryable();
    
    Task<Transport?> GetByIdModeAsync(long idMode, CancellationToken cancellationToken);

    Task<List<TransportForCapacityDto>> GetTransportsForCapacitiesAsync(LocalDate dateFrom, CancellationToken cancellationToken);
    
    Task<List<TransportHistory>> GetTransportHistoryNoTrackingAsync(Guid transportId, CancellationToken cancellationToken);
    
    Task<string?> GetTransportTerminalCodeAsync(Guid transportId, CancellationToken cancellationToken);
}