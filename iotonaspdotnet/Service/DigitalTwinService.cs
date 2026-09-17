using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IDigitalTwinService
{
    Task<DigitalTwin?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<DigitalTwin>> GetAll(CancellationToken cancellationToken);
    Task Create(DigitalTwinRequest request , CancellationToken cancellationToken);
    Task<bool> Update(DigitalTwinRequest request, CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);


}

public class DigitalTwinService : IDigitalTwinService
{
    private readonly IDigitalTwinRepository _repository;

    public DigitalTwinService(
        IDigitalTwinRepository repository )
    {
        _repository = repository;
    }

    public Task<DigitalTwin?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<DigitalTwin>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(DigitalTwinRequest request, CancellationToken cancellationToken)
    {
        var ioTDevice = await _ioTDevices.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.Device is not null)
        {
            throw new InvalidOperationException("IoTDevice:Device already has a(n) DigitalTwin (1:1 relationship).");
        }
        var gateway = await _gateways.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Gateway not found.");

        if (gateway.Gateway is not null)
        {
            throw new InvalidOperationException("Gateway:Gateway already has a(n) DigitalTwin (1:1 relationship).");
        }
        var twinTemplate = await _twinTemplates.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("TwinTemplate not found.");

        if (twinTemplate.Template is not null)
        {
            throw new InvalidOperationException("TwinTemplate:Template already has a(n) DigitalTwin (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(DigitalTwinRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.TwinId = request.TwinId;
        existing.DesiredStateVersion = request.DesiredStateVersion;
        existing.ReportedStateVersion = request.ReportedStateVersion;
        existing.LastSyncAt = request.LastSyncAt;
        existing.Device = request.Device;
        existing.Gateway = request.Gateway;
        existing.Template = request.Template;
        existing.ChangeEvents = request.ChangeEvents;
        await _repository.UpdateAsync(existing, cancellationToken);
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

    public async Task<bool> DeleteAsync(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }


}
