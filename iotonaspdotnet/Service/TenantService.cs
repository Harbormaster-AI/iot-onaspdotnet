using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service

public interface ITenantService
{
    Task<Tenant?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Tenant>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(Tenant tenant, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Tenant tenant, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class TenantService : ITenantService
{
    private readonly ITenantRepository _repository;

    public TenantService(
        ITenantRepository repository )
    {
        _repository = repository;
    }

    public Task<Tenant?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<Tenant>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(Tenant tenant, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(tenant, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Tenant tenant, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(tenant.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a alertRule who already has another tenant.
        if (existing.AlertRuleId != tenant.AlertRuleId)
        {
            var target;
        }

        existing.Name = tenant.Name;
        existing.TenantType = tenant.TenantType;

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
