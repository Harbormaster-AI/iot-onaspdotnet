using iotonaspdotnet.Domain
using iotonaspdotnet.Persistence
using iotonaspdotnet.Persistence.IoTDevices;
using iotonaspdotnet.Persistence.Gateways;
using iotonaspdotnet.Persistence.SimCards;

namespace iotonaspdotnet.Service

public interface INetworkProfileService
{
    Task<NetworkProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<NetworkProfile>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(NetworkProfile networkProfile, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(NetworkProfile networkProfile, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class NetworkProfileService : INetworkProfileService
{
    private readonly INetworkProfileRepository _repository;
    private readonly IIoTDeviceRepository _ioTDevices;
    private readonly IGatewayRepository _gateways;
    private readonly ISimCardRepository _simCards;

    public NetworkProfileService(
        IIoTDeviceRepository ioTDevices,
        IGatewayRepository gateways,
        ISimCardRepository simCards,
        INetworkProfileRepository repository )
    {
        _repository = repository;
        _simCards = simCards;
        _simCards = simCards;
        _simCards = simCards;
    }

    public Task<NetworkProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<NetworkProfile>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(NetworkProfile networkProfile, CancellationToken cancellationToken)
    {
        var ioTDevice = await _ioTDevices.GetByIdAsync(networkProfile.IoTDeviceId, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.NetworkProfile is not null)
        {
            throw new InvalidOperationException("IoTDevice already has a(n) networkProfile (1:1 relationship).");
        }

        var gateway = await _gateways.GetByIdAsync(networkProfile.GatewayId, cancellationToken)
            ?? throw new InvalidOperationException("Gateway not found.");

        if (gateway.NetworkProfile is not null)
        {
            throw new InvalidOperationException("Gateway already has a(n) networkProfile (1:1 relationship).");
        }

        var simCard = await _simCards.GetByIdAsync(networkProfile.SimCardId, cancellationToken)
            ?? throw new InvalidOperationException("SimCard not found.");

        if (simCard.NetworkProfile is not null)
        {
            throw new InvalidOperationException("SimCard already has a(n) networkProfile (1:1 relationship).");
        }

        await _repository.AddAsync(networkProfile, cancellationToken);
    }

    public async Task<bool> UpdateAsync(NetworkProfile networkProfile, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(networkProfile.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a simCard who already has another networkProfile.
        if (existing.SimCardId != networkProfile.SimCardId)
        {
            var target;
            target = await _ioTDevices.GetByIdAsync(networkProfile.IoTDeviceId, cancellationToken)
                ?? throw new InvalidOperationException("IoTDevice not found.");

            if (target.NetworkProfile is not null && target.NetworkProfile.Id != existing.Id)
            {
                throw new InvalidOperationException("Target ioTDevice already has an networkProfile (1:1 relationship).");
            }

        }
            target = await _gateways.GetByIdAsync(networkProfile.GatewayId, cancellationToken)
                ?? throw new InvalidOperationException("Gateway not found.");

            if (target.NetworkProfile is not null && target.NetworkProfile.Id != existing.Id)
            {
                throw new InvalidOperationException("Target gateway already has an networkProfile (1:1 relationship).");
            }

        }
            target = await _simCards.GetByIdAsync(networkProfile.SimCardId, cancellationToken)
                ?? throw new InvalidOperationException("SimCard not found.");

            if (target.NetworkProfile is not null && target.NetworkProfile.Id != existing.Id)
            {
                throw new InvalidOperationException("Target simCard already has an networkProfile (1:1 relationship).");
            }

        }

        existing.attributeName = networkProfile.attributeName;
        existing.attributeName = networkProfile.attributeName;
        existing.attributeName = networkProfile.attributeName;
        existing.attributeName = networkProfile.attributeName;

        existing.IoTDeviceId = networkProfile.IoTDeviceId;
        existing.GatewayId = networkProfile.GatewayId;
        existing.SimCardId = networkProfile.SimCardId;
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
