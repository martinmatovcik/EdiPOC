using System.Linq.Expressions;
using MIS3.Trucks.Common.Codebooks.Abstraction;

namespace EdiPOC.Codebook.Repositories;

public interface ICodebookRepository
{
    Task<List<T>> GetAllOfTypeAsync<T>(CancellationToken cancellationToken) where T : CodebookEntity;
    
    Task<List<T>> GetAllOfTypeAsync<T>(Expression<Func<T, bool>> filter, CancellationToken cancellationToken) where T : CodebookEntity;
    
    Task<T?> GetByCodebookIdAsync<T>(Guid codebookId, CancellationToken cancellationToken) where T : CodebookEntity;
    
    Task<List<T>> GetByCodebookIdsAsync<T>(List<Guid> codebookIds, CancellationToken cancellationToken) where T : CodebookEntity;
    
    void Add<T>(T entity) where T : CodebookEntity;
    
    Task<List<ServiceTrade>> GetServiceTradesByCodesAsync(IEnumerable<string> serviceTradeCodes, CancellationToken cancellationToken);
    
    Task<ContainerType> GetContainerTypeByCodeAsync(string containerTypeCode, CancellationToken cancellationToken);

    Task<BusinessPartner> GetBusinessPartnerBySapNumberAsync(string sapNumber, CancellationToken cancellationToken);
}