using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Persistence.Tenants;

namespace iotonaspdotnet.Service;

public interface IAlertRuleService
{
    Task<AlertRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AlertRule>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(AlertRule alertRule, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(AlertRule alertRule, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class AlertRuleService : IAlertRuleService
{
    private readonly IAlertRuleRepository _repository;
    private readonly ITenantRepository _tenants;

    public AlertRuleService(
        ITenantRepository tenants,
        IAlertRuleRepository repository )
    {
        _repository = repository;
        _tenants = tenants;
    }

    public Task<AlertRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<AlertRule>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(AlertRule alertRule, CancellationToken cancellationToken)
    {
        var tenant = await _tenants.GetByIdAsync(alertRule.TenantId, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.AlertRule is not null)
        {
            throw new InvalidOperationException("Tenant already has a(n) alertRule (1:1 relationship).");
        }

        await _repository.AddAsync(alertRule, cancellationToken);
    }

    public async Task<bool> UpdateAsync(AlertRule alertRule, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(alertRule.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a tenant who already has another alertRule.
        if (existing.TenantId != alertRule.TenantId)
        {
            var Tenant = await _tenants.GetByIdAsync(alertRule.TenantId, cancellationToken)
                ?? throw new InvalidOperationException("Tenant not found.");

            if (Tenant.AlertRule is not null && Tenant.AlertRule.Id != existing.Id)
            {
                throw new InvalidOperationException("Target tenant already has an alertRule (1:1 relationship).");
            }
        }

        existing.Name = alertRule.Name;
        existing.Expression = alertRule.Expression;
        existing.Severity = alertRule.Severity;

        existing.TenantId = alertRule.TenantId;
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
