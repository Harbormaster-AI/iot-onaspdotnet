using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface IActuatorInstanceService {

    Task Create(ActuatorInstance model , CancellationToken cancellationToken);
    Task<bool> Update(ActuatorInstance model, CancellationToken cancellationToken);
    Task<ActuatorInstance?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ActuatorInstance>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToSupportedCommands(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSupportedCommands(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ActuatorInstanceService : IActuatorInstanceService
{
    private readonly IActuatorInstanceRepository _repository;

    public ActuatorInstanceService(
        IActuatorInstanceRepository repository )
    {
        _repository = repository;
    }


    public async Task Create(ActuatorInstance model, CancellationToken cancellationToken)
    {

         await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(ActuatorInstance model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = model.Name;
        existing.CommandTopic = model.CommandTopic;
        existing.ActuatorType = model.ActuatorType;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<ActuatorInstance?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ActuatorInstance>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToSupportedCommands(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromSupportedCommands(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
