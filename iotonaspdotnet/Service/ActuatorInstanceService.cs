using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IActuatorInstanceService
{
    Task<ActuatorInstance?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ActuatorInstance>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(ActuatorInstance actuatorInstance, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(ActuatorInstance actuatorInstance, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class ActuatorInstanceService : IActuatorInstanceService
{
    private readonly IActuatorInstanceRepository _repository;
    private readonly IIoTDeviceRepository _ioTDevices;

    public ActuatorInstanceService(
        IIoTDeviceRepository ioTDevices,
        IActuatorInstanceRepository repository )
    {
        _repository = repository;
        _ioTDevices = ioTDevices;
    }

    public Task<ActuatorInstance?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<ActuatorInstance>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(ActuatorInstance actuatorInstance, CancellationToken cancellationToken)
    {
        var ioTDevice = await _ioTDevices.GetByIdAsync(actuatorInstance.Id, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.Device is not null)
        {
            throw new InvalidOperationException("IoTDevice already has a(n) actuatorInstance (1:1 relationship).");
        }
        await _repository.AddAsync(actuatorInstance, cancellationToken);
    }

    public async Task<bool> UpdateAsync(ActuatorInstance actuatorInstance, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(actuatorInstance.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a ioTDevice who already has another actuatorInstance.
        if (existing.Id != actuatorInstance.Id)
        {
            var Device = await _ioTDevices.GetByIdAsync(actuatorInstance.Id, cancellationToken)
                ?? throw new InvalidOperationException("IoTDevice not found.");

            if (Device.ActuatorInstance is not null && Device.ActuatorInstance.Id != existing.Id)
            {
                throw new InvalidOperationException("Target ioTDevice already has an actuatorInstance (1:1 relationship).");
            }
        }

        existing.Name = actuatorInstance.Name;
        existing.CommandTopic = actuatorInstance.CommandTopic;
        existing.ActuatorType = actuatorInstance.ActuatorType;

        existing.Id = actuatorInstance.Id;
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
