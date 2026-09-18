using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface ICommandDefinitionService {

    Task Create(CommandDefinition model , CancellationToken cancellationToken);
    Task<bool> Update(CommandDefinition model, CancellationToken cancellationToken);
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


    public async Task Create(CommandDefinition model, CancellationToken cancellationToken)
    {

         await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(CommandDefinition model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = model.Name;
        existing.RequestSchemaUri = model.RequestSchemaUri;
        existing.ResponseSchemaUri = model.ResponseSchemaUri;
        existing.TimeoutSeconds = model.TimeoutSeconds;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<CommandDefinition?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<CommandDefinition>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

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

    public async Task<bool> AssignDeviceModel(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDeviceModel(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToActuators(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromActuators(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToCommandInvocations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCommandInvocations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
