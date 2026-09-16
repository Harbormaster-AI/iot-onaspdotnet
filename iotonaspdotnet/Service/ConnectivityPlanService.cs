using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Persistence.Tenants;

namespace iotonaspdotnet.Service

public interface IConnectivityPlanService
{
    Task<ConnectivityPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ConnectivityPlan>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(ConnectivityPlan connectivityPlan, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(ConnectivityPlan connectivityPlan, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class ConnectivityPlanService : IConnectivityPlanService
{
    private readonly IConnectivityPlanRepository _repository;
    private readonly ITenantRepository _tenants;

    public ConnectivityPlanService(
        ITenantRepository tenants,
        IConnectivityPlanRepository repository )
    {
        _repository = repository;
        _tenants = tenants;
    }

    public Task<ConnectivityPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<ConnectivityPlan>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(ConnectivityPlan connectivityPlan, CancellationToken cancellationToken)
    {
        var tenant = await _tenants.GetByIdAsync(connectivityPlan.TenantId, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.ConnectivityPlan is not null)
        {
            throw new InvalidOperationException("Tenant already has a(n) connectivityPlan (1:1 relationship).");
        }

        await _repository.AddAsync(connectivityPlan, cancellationToken);
    }

    public async Task<bool> UpdateAsync(ConnectivityPlan connectivityPlan, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(connectivityPlan.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a tenant who already has another connectivityPlan.
        if (existing.TenantId != connectivityPlan.TenantId)
        {
            var target;
            target = await _tenants.GetByIdAsync(connectivityPlan.TenantId, cancellationToken)
                ?? throw new InvalidOperationException("Tenant not found.");

            if (target.ConnectivityPlan is not null && target.ConnectivityPlan.Id != existing.Id)
            {
                throw new InvalidOperationException("Target tenant already has an connectivityPlan (1:1 relationship).");
            }
        }

        existing.Name = connectivityPlan.Name;
        existing.DataCapMB = connectivityPlan.DataCapMB;
        existing.BillingCycleDays = connectivityPlan.BillingCycleDays;

        existing.TenantId = connectivityPlan.TenantId;
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
