using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface IAlertService {

    Task Create(AlertRequest request , CancellationToken cancellationToken);
    Task<bool> Update(AlertRequest request, CancellationToken cancellationToken);
    Task<Alert?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Alert>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignAlertRule(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAlertRule(AssociationRequest request, CancellationToken cancellationToken);


}

public class AlertService : IAlertService
{
    private readonly IAlertRepository _repository;

    public AlertService(
        IAlertRepository repository )
    {
        _repository = repository;
    }

    public Task<Alert?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<Alert>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(AlertRequest request, CancellationToken cancellationToken)
    {
        var ioTDevice = await _ioTDevices.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.Device is not null)
        {
            throw new InvalidOperationException("IoTDevice:Device already has a(n) Alert (1:1 relationship).");
        }
        var alertRule = await _alertRules.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("AlertRule not found.");

        if (alertRule.AlertRule is not null)
        {
            throw new InvalidOperationException("AlertRule:AlertRule already has a(n) Alert (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(AlertRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.RaisedAt = request.RaisedAt;
        existing.ClearedAt = request.ClearedAt;
        existing.Message = request.Message;
        existing.Device = request.Device;
        existing.AlertRule = request.AlertRule;
        existing.Status = request.Status;

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

    public async Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignAlertRule(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignAlertRule(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
