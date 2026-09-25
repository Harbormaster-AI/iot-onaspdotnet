using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface IAccessPolicyRepository
{
    Task<AccessPolicy?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AccessPolicy>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AccessPolicy accessPolicy, CancellationToken cancellationToken);
    Task UpdateAsync(AccessPolicy accessPolicy, CancellationToken cancellationToken);
    Task DeleteAsync(AccessPolicy accessPolicy, CancellationToken cancellationToken);

    Task AddToApiKeysAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromApiKeysAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToUsersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromUsersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
