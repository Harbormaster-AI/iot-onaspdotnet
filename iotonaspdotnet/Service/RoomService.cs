using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IRoomService
{
    Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Room>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(Room room, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Room room, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class RoomService : IRoomService
{
    private readonly IRoomRepository _repository;
    private readonly IFloorRepository _floors;

    public RoomService(
        IFloorRepository floors,
        IRoomRepository repository )
    {
        _repository = repository;
        _floors = floors;
    }

    public Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<Room>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(Room room, CancellationToken cancellationToken)
    {
        var floor = await _floors.GetByIdAsync(room.Id, cancellationToken)
            ?? throw new InvalidOperationException("Floor not found.");

        if (floor.Room is not null)
        {
            throw new InvalidOperationException("Floor already has a(n) room (1:1 relationship).");
        }

        await _repository.AddAsync(room, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Room room, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(room.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a floor who already has another room.
        if (existing.Id != room.Id)
        {
            var Floor = await _floors.GetByIdAsync(room.Id, cancellationToken)
                ?? throw new InvalidOperationException("Floor not found.");

            if (Floor.Room is not null && Floor.Room.Id != existing.Id)
            {
                throw new InvalidOperationException("Target floor already has an room (1:1 relationship).");
            }
        }

        existing.Name = room.Name;

        existing.Id = room.Id;
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
