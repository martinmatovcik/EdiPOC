using MIS3.Trucks.Common.Domain.Entity;

namespace EdiPOC.Cmr.Repository;

public interface ICmrRepository : IEntityRepository<Cmr>
{
    ///  <summary>
    /// Gets a queryable collection of Cmr entities.
    /// </summary>
    IQueryable<Cmr> GetQueryable();

    /// <summary>
    /// Gets the next available CMR number for the specified prefix asynchronously.
    /// </summary>
    /// <param name="cmrPrefix">The prefix for the CMR number.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>The task result contains the next available CMR number.</returns>
    Task<int> GetNextCmrNumberAsync(string cmrPrefix, CancellationToken cancellationToken);

    Task<List<Cmr>> GetByIdsAsync(List<Guid> cmrIds, CancellationToken cancellationToken);
}