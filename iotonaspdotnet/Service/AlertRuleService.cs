using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IAlertRuleService

    Task Create(AlertRuleRequest request , CancellationToken cancellationToken);
    Task<bool> Update(AlertRuleRequest request, CancellationToken cancellationToken);
    Task<AlertRule?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<AlertRule>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignTenant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTenant(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToStreams(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromStreams(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAlerts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAlerts(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class AlertRuleService : IAlertRuleService
{
    private readonly IAlertRuleRepository _repository;

    public AlertRuleService(
        IAlertRuleRepository repository )
    {
        _repository = repository;
    }

    public Task<AlertRule?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<AlertRule>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(AlertRuleRequest request, CancellationToken cancellationToken)
    {
        var tenant = await _tenants.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.Tenant is not null)
        {
            throw new InvalidOperationException("Tenant:Tenant already has a(n) AlertRule (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(AlertRuleRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = request.Name;
        existing.Expression = request.Expression;
        existing.Tenant = request.Tenant;
        existing.Streams = request.Streams;
        existing.Alerts = request.Alerts;
        existing.Severity = request.Severity;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }

    Task<bool> AssignTenant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignTenant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    Task<bool> AddToStreams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromStreams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AddToAlerts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromAlerts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
