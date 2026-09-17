using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IAlertService
{
    Task<Alert?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Alert>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(Alert alert, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Alert alert, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class AlertService : IAlertService
{
    private readonly IAlertRepository _repository;
    private readonly IIoTDeviceRepository _ioTDevices;
    private readonly IAlertRuleRepository _alertRules;

    public AlertService(
        IIoTDeviceRepository ioTDevices,
        IAlertRuleRepository alertRules,
        IAlertRepository repository )
    {
        _repository = repository;
        _ioTDevices = ioTDevices;
        _alertRules = alertRules;
    }

    public Task<Alert?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<Alert>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(Alert alert, CancellationToken cancellationToken)
    {
        var ioTDevice = await _ioTDevices.GetByIdAsync(alert.Id, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.Device is not null)
        {
            throw new InvalidOperationException("IoTDevice already has a(n) alert (1:1 relationship).");
        }
        var alertRule = await _alertRules.GetByIdAsync(alert.Id, cancellationToken)
            ?? throw new InvalidOperationException("AlertRule not found.");

        if (alertRule.AlertRule is not null)
        {
            throw new InvalidOperationException("AlertRule already has a(n) alert (1:1 relationship).");
        }
        await _repository.AddAsync(alert, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Alert alert, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(alert.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a alertRule who already has another alert.
        if (existing.Id != alert.Id)
        {
            var Device = await _ioTDevices.GetByIdAsync(alert.Id, cancellationToken)
                ?? throw new InvalidOperationException("IoTDevice not found.");

            if (Device.Alert is not null && Device.Alert.Id != existing.Id)
            {
                throw new InvalidOperationException("Target ioTDevice already has an alert (1:1 relationship).");
            }
            var AlertRule = await _alertRules.GetByIdAsync(alert.Id, cancellationToken)
                ?? throw new InvalidOperationException("AlertRule not found.");

            if (AlertRule.Alert is not null && AlertRule.Alert.Id != existing.Id)
            {
                throw new InvalidOperationException("Target alertRule already has an alert (1:1 relationship).");
            }
        }

        existing.RaisedAt = alert.RaisedAt;
        existing.ClearedAt = alert.ClearedAt;
        existing.Message = alert.Message;
        existing.Status = alert.Status;

        existing.Id = alert.Id;
        existing.Id = alert.Id;
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
