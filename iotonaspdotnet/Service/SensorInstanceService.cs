using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface ISensorInstanceService

    Task Create(SensorInstanceRequest request , CancellationToken cancellationToken);
    Task<bool> Update(SensorInstanceRequest request, CancellationToken cancellationToken);
    Task<SensorInstance?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<SensorInstance>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToTelemetryStreams(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTelemetryStreams(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class SensorInstanceService : ISensorInstanceService
{
    private readonly ISensorInstanceRepository _repository;

    public SensorInstanceService(
        ISensorInstanceRepository repository )
    {
        _repository = repository;
    }

    public Task<SensorInstance?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<SensorInstance>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(SensorInstanceRequest request, CancellationToken cancellationToken)
    {
        var ioTDevice = await _ioTDevices.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.Device is not null)
        {
            throw new InvalidOperationException("IoTDevice:Device already has a(n) SensorInstance (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(SensorInstanceRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = request.Name;
        existing.Unit = request.Unit;
        existing.SamplingIntervalMs = request.SamplingIntervalMs;
        existing.Device = request.Device;
        existing.TelemetryStreams = request.TelemetryStreams;
        existing.SensorType = request.SensorType;

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

    Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    Task<bool> AddToTelemetryStreams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromTelemetryStreams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
