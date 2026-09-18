using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface IDigitalTwinService {

    Task Create(DigitalTwin model , CancellationToken cancellationToken);
    Task<bool> Update(DigitalTwin model, CancellationToken cancellationToken);
    Task<DigitalTwin?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<DigitalTwin>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignGateway(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignGateway(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignTemplate(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTemplate(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToChangeEvents(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromChangeEvents(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class DigitalTwinService : IDigitalTwinService
{
    private readonly IDigitalTwinRepository _repository;

    public DigitalTwinService(
        IDigitalTwinRepository repository )
    {
        _repository = repository;
    }


    public async Task Create(DigitalTwin model, CancellationToken cancellationToken)
    {

 
 
         await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(DigitalTwin model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.TwinId = model.TwinId;
        existing.DesiredStateVersion = model.DesiredStateVersion;
        existing.ReportedStateVersion = model.ReportedStateVersion;
        existing.LastSyncAt = model.LastSyncAt;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<DigitalTwin?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<DigitalTwin>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignGateway(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignGateway(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignTemplate(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignTemplate(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToChangeEvents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromChangeEvents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
