using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IDigitalTwinService
{
    Task<DigitalTwin?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DigitalTwin>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(DigitalTwin digitalTwin, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(DigitalTwin digitalTwin, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class DigitalTwinService : IDigitalTwinService
{
    private readonly IDigitalTwinRepository _repository;
    private readonly IIoTDeviceRepository _ioTDevices;
    private readonly IGatewayRepository _gateways;
    private readonly ITwinTemplateRepository _twinTemplates;

    public DigitalTwinService(
        IIoTDeviceRepository ioTDevices,
        IGatewayRepository gateways,
        ITwinTemplateRepository twinTemplates,
        IDigitalTwinRepository repository )
    {
        _repository = repository;
        _twinTemplates = twinTemplates;
        _twinTemplates = twinTemplates;
        _twinTemplates = twinTemplates;
    }

    public Task<DigitalTwin?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<DigitalTwin>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(DigitalTwin digitalTwin, CancellationToken cancellationToken)
    {
        var ioTDevice = await _ioTDevices.GetByIdAsync(digitalTwin.Id, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.DigitalTwin is not null)
        {
            throw new InvalidOperationException("IoTDevice already has a(n) digitalTwin (1:1 relationship).");
        }

        var gateway = await _gateways.GetByIdAsync(digitalTwin.Id, cancellationToken)
            ?? throw new InvalidOperationException("Gateway not found.");

        if (gateway.DigitalTwin is not null)
        {
            throw new InvalidOperationException("Gateway already has a(n) digitalTwin (1:1 relationship).");
        }

        var twinTemplate = await _twinTemplates.GetByIdAsync(digitalTwin.Id, cancellationToken)
            ?? throw new InvalidOperationException("TwinTemplate not found.");

        if (twinTemplate.DigitalTwin is not null)
        {
            throw new InvalidOperationException("TwinTemplate already has a(n) digitalTwin (1:1 relationship).");
        }

        await _repository.AddAsync(digitalTwin, cancellationToken);
    }

    public async Task<bool> UpdateAsync(DigitalTwin digitalTwin, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(digitalTwin.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a twinTemplate who already has another digitalTwin.
        if (existing.Id != digitalTwin.Id)
        {
            var Device = await _ioTDevices.GetByIdAsync(digitalTwin.Id, cancellationToken)
                ?? throw new InvalidOperationException("IoTDevice not found.");

            if (Device.DigitalTwin is not null && Device.DigitalTwin.Id != existing.Id)
            {
                throw new InvalidOperationException("Target ioTDevice already has an digitalTwin (1:1 relationship).");
            }
            var Gateway = await _gateways.GetByIdAsync(digitalTwin.Id, cancellationToken)
                ?? throw new InvalidOperationException("Gateway not found.");

            if (Gateway.DigitalTwin is not null && Gateway.DigitalTwin.Id != existing.Id)
            {
                throw new InvalidOperationException("Target gateway already has an digitalTwin (1:1 relationship).");
            }
            var Template = await _twinTemplates.GetByIdAsync(digitalTwin.Id, cancellationToken)
                ?? throw new InvalidOperationException("TwinTemplate not found.");

            if (Template.DigitalTwin is not null && Template.DigitalTwin.Id != existing.Id)
            {
                throw new InvalidOperationException("Target twinTemplate already has an digitalTwin (1:1 relationship).");
            }
        }

        existing.TwinId = digitalTwin.TwinId;
        existing.DesiredStateVersion = digitalTwin.DesiredStateVersion;
        existing.ReportedStateVersion = digitalTwin.ReportedStateVersion;
        existing.LastSyncAt = digitalTwin.LastSyncAt;

        existing.Id = digitalTwin.Id;
        existing.Id = digitalTwin.Id;
        existing.Id = digitalTwin.Id;
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
