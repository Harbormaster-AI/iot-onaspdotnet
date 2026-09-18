using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IEdgeApplicationService

    Task Create(EdgeApplicationRequest request , CancellationToken cancellationToken);
    Task<bool> Update(EdgeApplicationRequest request, CancellationToken cancellationToken);
    Task<EdgeApplication?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<EdgeApplication>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignGateway(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignGateway(AssociationRequest request, CancellationToken cancellationToken);


}

public class EdgeApplicationService : IEdgeApplicationService
{
    private readonly IEdgeApplicationRepository _repository;

    public EdgeApplicationService(
        IEdgeApplicationRepository repository )
    {
        _repository = repository;
    }

    public Task<EdgeApplication?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<EdgeApplication>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(EdgeApplicationRequest request, CancellationToken cancellationToken)
    {
        var gateway = await _gateways.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Gateway not found.");

        if (gateway.Gateway is not null)
        {
            throw new InvalidOperationException("Gateway:Gateway already has a(n) EdgeApplication (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(EdgeApplicationRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = request.Name;
        existing.Version = request.Version;
        existing.Image = request.Image;
        existing.Gateway = request.Gateway;
        existing.Status = request.Status;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

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

    Task<bool> AssignGateway(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignGateway(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
