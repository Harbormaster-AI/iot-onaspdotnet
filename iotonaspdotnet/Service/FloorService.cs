using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface IFloorService {

    Task Create(Floor model , CancellationToken cancellationToken);
    Task<bool> Update(Floor model, CancellationToken cancellationToken);
    Task<Floor?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Floor>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignBuilding(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignBuilding(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToRooms(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRooms(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class FloorService : IFloorService
{
    private readonly IFloorRepository _repository;

    public FloorService(
        IFloorRepository repository )
    {
        _repository = repository;
    }


    public async Task Create(Floor model, CancellationToken cancellationToken)
    {

         await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(Floor model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = model.Name;
        existing.Level = model.Level;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<Floor?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Floor>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignBuilding(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignBuilding(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToRooms(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromRooms(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
