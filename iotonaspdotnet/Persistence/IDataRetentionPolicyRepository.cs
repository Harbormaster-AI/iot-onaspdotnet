using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface IDataRetentionPolicyRepository
{
    Task<DataRetentionPolicy?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DataRetentionPolicy>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DataRetentionPolicy dataRetentionPolicy, CancellationToken cancellationToken);
    Task UpdateAsync(DataRetentionPolicy dataRetentionPolicy, CancellationToken cancellationToken);
    Task DeleteAsync(DataRetentionPolicy dataRetentionPolicy, CancellationToken cancellationToken);

    Task AddToStreamsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromStreamsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
