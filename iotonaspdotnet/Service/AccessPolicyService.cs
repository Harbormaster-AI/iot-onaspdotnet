using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Persistence.Tenants;

namespace iotonaspdotnet.Service;

public interface IAccessPolicyService
{
    Task<AccessPolicy?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AccessPolicy>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(AccessPolicy accessPolicy, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(AccessPolicy accessPolicy, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class AccessPolicyService : IAccessPolicyService
{
    private readonly IAccessPolicyRepository _repository;
    private readonly ITenantRepository _tenants;

    public AccessPolicyService(
        ITenantRepository tenants,
        IAccessPolicyRepository repository )
    {
        _repository = repository;
        _tenants = tenants;
    }

    public Task<AccessPolicy?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<AccessPolicy>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(AccessPolicy accessPolicy, CancellationToken cancellationToken)
    {
        var tenant = await _tenants.GetByIdAsync(accessPolicy.TenantId, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.AccessPolicy is not null)
        {
            throw new InvalidOperationException("Tenant already has a(n) accessPolicy (1:1 relationship).");
        }

        await _repository.AddAsync(accessPolicy, cancellationToken);
    }

    public async Task<bool> UpdateAsync(AccessPolicy accessPolicy, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(accessPolicy.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a tenant who already has another accessPolicy.
        if (existing.TenantId != accessPolicy.TenantId)
        {
            var Tenant = await _tenants.GetByIdAsync(accessPolicy.TenantId, cancellationToken)
                ?? throw new InvalidOperationException("Tenant not found.");

            if (Tenant.AccessPolicy is not null && Tenant.AccessPolicy.Id != existing.Id)
            {
                throw new InvalidOperationException("Target tenant already has an accessPolicy (1:1 relationship).");
            }
        }

        existing.Name = accessPolicy.Name;
        existing.Scope = accessPolicy.Scope;
        existing.ExpiresAt = accessPolicy.ExpiresAt;

        existing.TenantId = accessPolicy.TenantId;
        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }
}
