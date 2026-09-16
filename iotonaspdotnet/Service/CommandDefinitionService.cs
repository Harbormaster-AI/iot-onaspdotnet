using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Persistence.DeviceModels;

namespace iotonaspdotnet.Service;

public interface ICommandDefinitionService
{
    Task<CommandDefinition?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CommandDefinition>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(CommandDefinition commandDefinition, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(CommandDefinition commandDefinition, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class CommandDefinitionService : ICommandDefinitionService
{
    private readonly ICommandDefinitionRepository _repository;
    private readonly IDeviceModelRepository _deviceModels;

    public CommandDefinitionService(
        IDeviceModelRepository deviceModels,
        ICommandDefinitionRepository repository )
    {
        _repository = repository;
        _deviceModels = deviceModels;
    }

    public Task<CommandDefinition?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<CommandDefinition>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(CommandDefinition commandDefinition, CancellationToken cancellationToken)
    {
        var deviceModel = await _deviceModels.GetByIdAsync(commandDefinition.DeviceModelId, cancellationToken)
            ?? throw new InvalidOperationException("DeviceModel not found.");

        if (deviceModel.CommandDefinition is not null)
        {
            throw new InvalidOperationException("DeviceModel already has a(n) commandDefinition (1:1 relationship).");
        }

        await _repository.AddAsync(commandDefinition, cancellationToken);
    }

    public async Task<bool> UpdateAsync(CommandDefinition commandDefinition, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(commandDefinition.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a deviceModel who already has another commandDefinition.
        if (existing.DeviceModelId != commandDefinition.DeviceModelId)
        {
            var DeviceModel = await _deviceModels.GetByIdAsync(commandDefinition.DeviceModelId, cancellationToken)
                ?? throw new InvalidOperationException("DeviceModel not found.");

            if (DeviceModel.CommandDefinition is not null && DeviceModel.CommandDefinition.Id != existing.Id)
            {
                throw new InvalidOperationException("Target deviceModel already has an commandDefinition (1:1 relationship).");
            }
        }

        existing.Name = commandDefinition.Name;
        existing.RequestSchemaUri = commandDefinition.RequestSchemaUri;
        existing.ResponseSchemaUri = commandDefinition.ResponseSchemaUri;
        existing.TimeoutSeconds = commandDefinition.TimeoutSeconds;

        existing.DeviceModelId = commandDefinition.DeviceModelId;
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
