using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface ICommandDefinitionService

    Task Create(CommandDefinitionRequest request , CancellationToken cancellationToken);
    Task<bool> Update(CommandDefinitionRequest request, CancellationToken cancellationToken);
    Task<CommandDefinition?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<CommandDefinition>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDeviceModel(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDeviceModel(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToActuators(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromActuators(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCommandInvocations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCommandInvocations(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class CommandDefinitionService : ICommandDefinitionService
{
    private readonly ICommandDefinitionRepository _repository;

    public CommandDefinitionService(
        ICommandDefinitionRepository repository )
    {
        _repository = repository;
    }

    public Task<CommandDefinition?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<CommandDefinition>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(CommandDefinitionRequest request, CancellationToken cancellationToken)
    {
        var deviceModel = await _deviceModels.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("DeviceModel not found.");

        if (deviceModel.DeviceModel is not null)
        {
            throw new InvalidOperationException("DeviceModel:DeviceModel already has a(n) CommandDefinition (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(CommandDefinitionRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = request.Name;
        existing.RequestSchemaUri = request.RequestSchemaUri;
        existing.ResponseSchemaUri = request.ResponseSchemaUri;
        existing.TimeoutSeconds = request.TimeoutSeconds;
        existing.DeviceModel = request.DeviceModel;
        existing.Actuators = request.Actuators;
        existing.CommandInvocations = request.CommandInvocations;

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

    Task<bool> AssignDeviceModel(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignDeviceModel(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    Task<bool> AddToActuators(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromActuators(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AddToCommandInvocations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromCommandInvocations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
