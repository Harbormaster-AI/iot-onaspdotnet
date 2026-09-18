using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface ISimCardRepository
{
    Task<SimCard?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<SimCard>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(SimCard simCard, CancellationToken cancellationToken);
    Task UpdateAsync(SimCard simCard, CancellationToken cancellationToken);
    Task DeleteAsync(SimCard simCard, CancellationToken cancellationToken);
}
