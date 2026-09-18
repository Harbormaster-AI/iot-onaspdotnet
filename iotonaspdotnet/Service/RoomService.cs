using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface IRoomService {

    Task Create(RoomRequest request , CancellationToken cancellationToken);
    Task<bool> Update(RoomRequest request, CancellationToken cancellationToken);
    Task<Room?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Room>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignFloor(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignFloor(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToDevices(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDevices(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToGateways(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromGateways(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class RoomService : IRoomService
{
    private readonly IRoomRepository _repository;

    public RoomService(
        IRoomRepository repository )
    {
        _repository = repository;
    }

    public Task<Room?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<Room>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(RoomRequest request, CancellationToken cancellationToken)
    {
        var floor = await _floors.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Floor not found.");

        if (floor.Floor is not null)
        {
            throw new InvalidOperationException("Floor:Floor already has a(n) Room (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(RoomRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = request.Name;
        existing.Floor = request.Floor;
        existing.Devices = request.Devices;
        existing.Gateways = request.Gateways;

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

    Task<bool> AssignFloor(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> UnassignFloor(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    Task<bool> AddToDevices(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromDevices(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AddToGateways(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromGateways(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
