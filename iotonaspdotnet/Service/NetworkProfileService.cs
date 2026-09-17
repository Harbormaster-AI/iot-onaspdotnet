using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface INetworkProfileService
{
    Task<NetworkProfile?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<NetworkProfile>> GetAll(CancellationToken cancellationToken);
    Task Create(NetworkProfileRequest request , CancellationToken cancellationToken);
    Task<bool> Update(NetworkProfileRequest request, CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);


}

public class NetworkProfileService : INetworkProfileService
{
    private readonly INetworkProfileRepository _repository;

    public NetworkProfileService(
        INetworkProfileRepository repository )
    {
        _repository = repository;
    }

    public Task<NetworkProfile?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<NetworkProfile>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(NetworkProfileRequest request, CancellationToken cancellationToken)
    {
        var ioTDevice = await _ioTDevices.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.Device is not null)
        {
            throw new InvalidOperationException("IoTDevice:Device already has a(n) NetworkProfile (1:1 relationship).");
        }
        var gateway = await _gateways.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Gateway not found.");

        if (gateway.Gateway is not null)
        {
            throw new InvalidOperationException("Gateway:Gateway already has a(n) NetworkProfile (1:1 relationship).");
        }
        var simCard = await _simCards.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("SimCard not found.");

        if (simCard.SimCard is not null)
        {
            throw new InvalidOperationException("SimCard:SimCard already has a(n) NetworkProfile (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(NetworkProfileRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.ProfileName = request.ProfileName;
        existing.Ssid = request.Ssid;
        existing.Apn = request.Apn;
        existing.Device = request.Device;
        existing.Gateway = request.Gateway;
        existing.SimCard = request.SimCard;
        existing.ConnectivityType = request.ConnectivityType;
        await _repository.UpdateAsync(existing, cancellationToken);
    }

        existing.ProfileName = networkProfile.ProfileName;
        existing.Ssid = networkProfile.Ssid;
        existing.Apn = networkProfile.Apn;
        existing.ConnectivityType = networkProfile.ConnectivityType;

        existing.Id = networkProfile.Id;
        existing.Id = networkProfile.Id;
        existing.Id = networkProfile.Id;
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
