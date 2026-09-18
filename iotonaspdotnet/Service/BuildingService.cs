using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IBuildingService

    Task Create(BuildingRequest request , CancellationToken cancellationToken);
    Task<bool> Update(BuildingRequest request, CancellationToken cancellationToken);
    Task<Building?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Building>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignSite(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSite(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToFloors(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromFloors(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class BuildingService : IBuildingService
{
    private readonly IBuildingRepository _repository;

    public BuildingService(
        IBuildingRepository repository )
    {
        _repository = repository;
    }

    public Task<Building?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<Building>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(BuildingRequest request, CancellationToken cancellationToken)
    {
        var site = await _sites.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Site not found.");

        if (site.Site is not null)
        {
            throw new InvalidOperationException("Site:Site already has a(n) Building (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(BuildingRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = request.Name;
        existing.Site = request.Site;
        existing.Floors = request.Floors;

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

    Task<bool> AssignSite(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignSite(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    Task<bool> AddToFloors(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromFloors(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
