using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface IGatewayService {

    Task Create(Gateway model , CancellationToken cancellationToken);
    Task<bool> Update(Gateway model, CancellationToken cancellationToken);
    Task<Gateway?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Gateway>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignSite(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSite(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignRoom(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRoom(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignDigitalTwin(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDigitalTwin(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToDevices(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDevices(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToEdgeApplications(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEdgeApplications(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCertificates(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCertificates(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToNetworkProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromNetworkProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class GatewayService : IGatewayService
{
    private readonly IGatewayRepository _repository;

    public GatewayService(
        IGatewayRepository repository )
    {
        _repository = repository;
    }


    public async Task Create(Gateway model, CancellationToken cancellationToken)
    {

 
 
         await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(Gateway model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.SoftwareVersion = model.SoftwareVersion;
        existing.Status = model.Status;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<Gateway?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Gateway>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignSite(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignSite(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignRoom(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRoom(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignDigitalTwin(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDigitalTwin(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToDevices(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDevices(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToEdgeApplications(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromEdgeApplications(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToCertificates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCertificates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToNetworkProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromNetworkProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
