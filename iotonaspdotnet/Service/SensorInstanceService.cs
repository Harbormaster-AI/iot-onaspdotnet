using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface ISensorInstanceService
{
    Task<SensorInstance?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<SensorInstance>> GetAll(CancellationToken cancellationToken);
    Task Create(SensorInstanceRequest request , CancellationToken cancellationToken);
    Task<bool> Update(SensorInstanceRequest request, CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);


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

        existing.Name = sensorInstance.Name;
        existing.Unit = sensorInstance.Unit;
        existing.SamplingIntervalMs = sensorInstance.SamplingIntervalMs;
        existing.SensorType = sensorInstance.SensorType;

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
