using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface INetworkProfileService {

    Task Create(NetworkProfile model , CancellationToken cancellationToken);
    Task<bool> Update(NetworkProfile model, CancellationToken cancellationToken);
    Task<NetworkProfile?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<NetworkProfile>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignGateway(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignGateway(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignSimCard(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSimCard(AssociationRequest request, CancellationToken cancellationToken);


}

public class NetworkProfileService : INetworkProfileService
{
    private readonly INetworkProfileRepository _repository;

    public NetworkProfileService(
        INetworkProfileRepository repository )
    {
        _repository = repository;
    }


    public async Task Create(NetworkProfile model, CancellationToken cancellationToken)
    {

 
 
         await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(NetworkProfile model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.ProfileName = model.ProfileName;
        existing.Ssid = model.Ssid;
        existing.Apn = model.Apn;
        existing.ConnectivityType = model.ConnectivityType;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<NetworkProfile?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<NetworkProfile>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignSimCard(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignSimCard(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
