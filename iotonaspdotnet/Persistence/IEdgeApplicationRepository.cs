using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface IEdgeApplicationRepository
{
    Task<EdgeApplication?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<EdgeApplication>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(EdgeApplication edgeApplication, CancellationToken cancellationToken);
    Task UpdateAsync(EdgeApplication edgeApplication, CancellationToken cancellationToken);
    Task DeleteAsync(EdgeApplication edgeApplication, CancellationToken cancellationToken);
}
