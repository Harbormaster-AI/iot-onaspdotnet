using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IEdgeApplicationService
{
    Task<EdgeApplication?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<EdgeApplication>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(EdgeApplication edgeApplication, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(EdgeApplication edgeApplication, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class EdgeApplicationService : IEdgeApplicationService
{
    private readonly IEdgeApplicationRepository _repository;
    private readonly IGatewayRepository _gateways;

    public EdgeApplicationService(
        IGatewayRepository gateways,
        IEdgeApplicationRepository repository )
    {
        _repository = repository;
        _gateways = gateways;
    }

    public Task<EdgeApplication?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<EdgeApplication>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(EdgeApplication edgeApplication, CancellationToken cancellationToken)
    {
        var gateway = await _gateways.GetByIdAsync(edgeApplication.GatewayId, cancellationToken)
            ?? throw new InvalidOperationException("Gateway not found.");

        if (gateway.EdgeApplication is not null)
        {
            throw new InvalidOperationException("Gateway already has a(n) edgeApplication (1:1 relationship).");
        }

        await _repository.AddAsync(edgeApplication, cancellationToken);
    }

    public async Task<bool> UpdateAsync(EdgeApplication edgeApplication, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(edgeApplication.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a gateway who already has another edgeApplication.
        if (existing.GatewayId != edgeApplication.GatewayId)
        {
            var Gateway = await _gateways.GetByIdAsync(edgeApplication.GatewayId, cancellationToken)
                ?? throw new InvalidOperationException("Gateway not found.");

            if (Gateway.EdgeApplication is not null && Gateway.EdgeApplication.Id != existing.Id)
            {
                throw new InvalidOperationException("Target gateway already has an edgeApplication (1:1 relationship).");
            }
        }

        existing.Name = edgeApplication.Name;
        existing.Version = edgeApplication.Version;
        existing.Image = edgeApplication.Image;
        existing.Status = edgeApplication.Status;

        existing.GatewayId = edgeApplication.GatewayId;
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
