using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface ISiteRepository
{
    Task<Site?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Site>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Site site, CancellationToken cancellationToken);
    Task UpdateAsync(Site site, CancellationToken cancellationToken);
    Task DeleteAsync(Site site, CancellationToken cancellationToken);
}
