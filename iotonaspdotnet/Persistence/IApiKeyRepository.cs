using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface IApiKeyRepository
{
    Task<ApiKey?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ApiKey>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ApiKey apiKey, CancellationToken cancellationToken);
    Task UpdateAsync(ApiKey apiKey, CancellationToken cancellationToken);
    Task DeleteAsync(ApiKey apiKey, CancellationToken cancellationToken);


}
