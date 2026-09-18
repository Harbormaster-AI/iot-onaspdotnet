using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface ITenantRepository
{
    Task<Tenant?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Tenant>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Tenant tenant, CancellationToken cancellationToken);
    Task UpdateAsync(Tenant tenant, CancellationToken cancellationToken);
    Task DeleteAsync(Tenant tenant, CancellationToken cancellationToken);
}
