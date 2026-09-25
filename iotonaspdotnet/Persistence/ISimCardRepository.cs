using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface ISimCardRepository
{
    Task<SimCard?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<SimCard>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(SimCard simCard, CancellationToken cancellationToken);
    Task UpdateAsync(SimCard simCard, CancellationToken cancellationToken);
    Task DeleteAsync(SimCard simCard, CancellationToken cancellationToken);

    Task AddToNetworkProfilesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromNetworkProfilesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
