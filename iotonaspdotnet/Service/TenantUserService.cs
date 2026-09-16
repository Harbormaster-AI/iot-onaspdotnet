using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Persistence.Tenants;

namespace iotonaspdotnet.Service;

public interface ITenantUserService
{
    Task<TenantUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TenantUser>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(TenantUser tenantUser, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(TenantUser tenantUser, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class TenantUserService : ITenantUserService
{
    private readonly ITenantUserRepository _repository;
    private readonly ITenantRepository _tenants;

    public TenantUserService(
        ITenantRepository tenants,
        ITenantUserRepository repository )
    {
        _repository = repository;
        _tenants = tenants;
    }

    public Task<TenantUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<TenantUser>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(TenantUser tenantUser, CancellationToken cancellationToken)
    {
        var tenant = await _tenants.GetByIdAsync(tenantUser.TenantId, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.TenantUser is not null)
        {
            throw new InvalidOperationException("Tenant already has a(n) tenantUser (1:1 relationship).");
        }

        await _repository.AddAsync(tenantUser, cancellationToken);
    }

    public async Task<bool> UpdateAsync(TenantUser tenantUser, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(tenantUser.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a tenant who already has another tenantUser.
        if (existing.TenantId != tenantUser.TenantId)
        {
            var Tenant = await _tenants.GetByIdAsync(tenantUser.TenantId, cancellationToken)
                ?? throw new InvalidOperationException("Tenant not found.");

            if (Tenant.TenantUser is not null && Tenant.TenantUser.Id != existing.Id)
            {
                throw new InvalidOperationException("Target tenant already has an tenantUser (1:1 relationship).");
            }
        }

        existing.FirstName = tenantUser.FirstName;
        existing.LastName = tenantUser.LastName;
        existing.Email = tenantUser.Email;
        existing.Role = tenantUser.Role;

        existing.TenantId = tenantUser.TenantId;
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
