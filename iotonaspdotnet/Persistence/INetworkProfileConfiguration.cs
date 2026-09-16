using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface INetworkProfileRepository
{
    Task<NetworkProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<NetworkProfile>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(NetworkProfile networkProfile, CancellationToken cancellationToken);
    Task UpdateAsync(NetworkProfile networkProfile, CancellationToken cancellationToken);
    Task DeleteAsync(NetworkProfile networkProfile, CancellationToken cancellationToken);
}
