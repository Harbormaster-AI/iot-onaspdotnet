using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IGatewayService
{
    Task<Gateway?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Gateway>> GetAll(CancellationToken cancellationToken);
    Task Create(GatewayRequest request , CancellationToken cancellationToken);
    Task<bool> Update(GatewayRequest request, CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);


}

public class GatewayService : IGatewayService
{
    private readonly IGatewayRepository _repository;

    public GatewayService(
        IGatewayRepository repository )
    {
        _repository = repository;
    }

    public Task<Gateway?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<Gateway>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(GatewayRequest request, CancellationToken cancellationToken)
    {
        var site = await _sites.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Site not found.");

        if (site.Site is not null)
        {
            throw new InvalidOperationException("Site:Site already has a(n) Gateway (1:1 relationship).");
        }
        var room = await _rooms.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Room not found.");

        if (room.Room is not null)
        {
            throw new InvalidOperationException("Room:Room already has a(n) Gateway (1:1 relationship).");
        }
        var digitalTwin = await _digitalTwins.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("DigitalTwin not found.");

        if (digitalTwin.DigitalTwin is not null)
        {
            throw new InvalidOperationException("DigitalTwin:DigitalTwin already has a(n) Gateway (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(GatewayRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.SoftwareVersion = request.SoftwareVersion
        existing.Site = request.Site
        existing.Room = request.Room
        existing.Devices = request.Devices
        existing.EdgeApplications = request.EdgeApplications
        existing.Certificates = request.Certificates
        existing.DigitalTwin = request.DigitalTwin
        existing.NetworkProfiles = request.NetworkProfiles
        existing.Status = request.Status
        await _repository.UpdateAsync(existing, cancellationToken);
    }

        existing.SoftwareVersion = gateway.SoftwareVersion;
        existing.Status = gateway.Status;

        existing.Id = gateway.Id;
        existing.Id = gateway.Id;
        existing.Id = gateway.Id;
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
