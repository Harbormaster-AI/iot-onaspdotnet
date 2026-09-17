using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IActuatorInstanceService
{
    Task<ActuatorInstance?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ActuatorInstance>> GetAll(CancellationToken cancellationToken);
    Task Create(ActuatorInstanceRequest request , CancellationToken cancellationToken);
    Task<bool> Update(ActuatorInstanceRequest request, CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);


}

public class ActuatorInstanceService : IActuatorInstanceService
{
    private readonly IActuatorInstanceRepository _repository;

    public ActuatorInstanceService(
        IActuatorInstanceRepository repository )
    {
        _repository = repository;
    }

    public Task<ActuatorInstance?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<ActuatorInstance>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(ActuatorInstanceRequest request, CancellationToken cancellationToken)
    {
        var ioTDevice = await _ioTDevices.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.Device is not null)
        {
            throw new InvalidOperationException("IoTDevice:Device already has a(n) ActuatorInstance (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(ActuatorInstanceRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = request.Name;
        existing.CommandTopic = request.CommandTopic;
        existing.Device = request.Device;
        existing.SupportedCommands = request.SupportedCommands;
        existing.ActuatorType = request.ActuatorType;
        await _repository.UpdateAsync(existing, cancellationToken);
    }

        existing.Name = actuatorInstance.Name;
        existing.CommandTopic = actuatorInstance.CommandTopic;
        existing.ActuatorType = actuatorInstance.ActuatorType;

        existing.Id = actuatorInstance.Id;
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
