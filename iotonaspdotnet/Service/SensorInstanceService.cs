using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface ISensorInstanceService
{
    Task<SensorInstance?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<SensorInstance>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(SensorInstance sensorInstance, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(SensorInstance sensorInstance, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class SensorInstanceService : ISensorInstanceService
{
    private readonly ISensorInstanceRepository _repository;
    private readonly IIoTDeviceRepository _ioTDevices;

    public SensorInstanceService(
        IIoTDeviceRepository ioTDevices,
        ISensorInstanceRepository repository )
    {
        _repository = repository;
        _ioTDevices = ioTDevices;
    }

    public Task<SensorInstance?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<SensorInstance>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(SensorInstance sensorInstance, CancellationToken cancellationToken)
    {
        var ioTDevice = await _ioTDevices.GetByIdAsync(sensorInstance.Id, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.Device is not null)
        {
            throw new InvalidOperationException("IoTDevice already has a(n) sensorInstance (1:1 relationship).");
        }
        await _repository.AddAsync(sensorInstance, cancellationToken);
    }

    public async Task<bool> UpdateAsync(SensorInstance sensorInstance, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(sensorInstance.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a ioTDevice who already has another sensorInstance.
        if (existing.Id != sensorInstance.Id)
        {
            var Device = await _ioTDevices.GetByIdAsync(sensorInstance.Id, cancellationToken)
                ?? throw new InvalidOperationException("IoTDevice not found.");

            if (Device.SensorInstance is not null && Device.SensorInstance.Id != existing.Id)
            {
                throw new InvalidOperationException("Target ioTDevice already has an sensorInstance (1:1 relationship).");
            }
        }

        existing.Name = sensorInstance.Name;
        existing.Unit = sensorInstance.Unit;
        existing.SamplingIntervalMs = sensorInstance.SamplingIntervalMs;
        existing.SensorType = sensorInstance.SensorType;

        existing.Id = sensorInstance.Id;
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
