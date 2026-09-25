using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface ITenantUserRepository
{
    Task<TenantUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TenantUser>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(TenantUser tenantUser, CancellationToken cancellationToken);
    Task UpdateAsync(TenantUser tenantUser, CancellationToken cancellationToken);
    Task DeleteAsync(TenantUser tenantUser, CancellationToken cancellationToken);

    Task AddToCommandInvocationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCommandInvocationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
