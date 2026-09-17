using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IGatewayService
{
    Task<Gateway?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Gateway>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(Gateway gateway, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Gateway gateway, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class GatewayService : IGatewayService
{
    private readonly IGatewayRepository _repository;
    private readonly ISiteRepository _sites;
    private readonly IRoomRepository _rooms;
    private readonly IDigitalTwinRepository _digitalTwins;

    public GatewayService(
        ISiteRepository sites,
        IRoomRepository rooms,
        IDigitalTwinRepository digitalTwins,
        IGatewayRepository repository )
    {
        _repository = repository;
        _sites = sites;
        _rooms = rooms;
        _digitalTwins = digitalTwins;
    }

    public Task<Gateway?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<Gateway>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(Gateway gateway, CancellationToken cancellationToken)
    {
        var site = await _sites.GetByIdAsync(gateway.Id, cancellationToken)
            ?? throw new InvalidOperationException("Site not found.");

        if (site.Site is not null)
        {
            throw new InvalidOperationException("Site already has a(n) gateway (1:1 relationship).");
        }
        var room = await _rooms.GetByIdAsync(gateway.Id, cancellationToken)
            ?? throw new InvalidOperationException("Room not found.");

        if (room.Room is not null)
        {
            throw new InvalidOperationException("Room already has a(n) gateway (1:1 relationship).");
        }
        var digitalTwin = await _digitalTwins.GetByIdAsync(gateway.Id, cancellationToken)
            ?? throw new InvalidOperationException("DigitalTwin not found.");

        if (digitalTwin.DigitalTwin is not null)
        {
            throw new InvalidOperationException("DigitalTwin already has a(n) gateway (1:1 relationship).");
        }
        await _repository.AddAsync(gateway, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Gateway gateway, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(gateway.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a digitalTwin who already has another gateway.
        if (existing.Id != gateway.Id)
        {
            var Site = await _sites.GetByIdAsync(gateway.Id, cancellationToken)
                ?? throw new InvalidOperationException("Site not found.");

            if (Site.Gateway is not null && Site.Gateway.Id != existing.Id)
            {
                throw new InvalidOperationException("Target site already has an gateway (1:1 relationship).");
            }
            var Room = await _rooms.GetByIdAsync(gateway.Id, cancellationToken)
                ?? throw new InvalidOperationException("Room not found.");

            if (Room.Gateway is not null && Room.Gateway.Id != existing.Id)
            {
                throw new InvalidOperationException("Target room already has an gateway (1:1 relationship).");
            }
            var DigitalTwin = await _digitalTwins.GetByIdAsync(gateway.Id, cancellationToken)
                ?? throw new InvalidOperationException("DigitalTwin not found.");

            if (DigitalTwin.Gateway is not null && DigitalTwin.Gateway.Id != existing.Id)
            {
                throw new InvalidOperationException("Target digitalTwin already has an gateway (1:1 relationship).");
            }
        }

        existing.SoftwareVersion = gateway.SoftwareVersion;
        existing.Status = gateway.Status;

        existing.Id = gateway.Id;
        existing.Id = gateway.Id;
        existing.Id = gateway.Id;
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
