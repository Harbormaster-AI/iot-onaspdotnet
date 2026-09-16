using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Persistence.Buildings;

namespace iotonaspdotnet.Service;

public interface IFloorService
{
    Task<Floor?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Floor>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(Floor floor, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Floor floor, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class FloorService : IFloorService
{
    private readonly IFloorRepository _repository;
    private readonly IBuildingRepository _buildings;

    public FloorService(
        IBuildingRepository buildings,
        IFloorRepository repository )
    {
        _repository = repository;
        _buildings = buildings;
    }

    public Task<Floor?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<Floor>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(Floor floor, CancellationToken cancellationToken)
    {
        var building = await _buildings.GetByIdAsync(floor.BuildingId, cancellationToken)
            ?? throw new InvalidOperationException("Building not found.");

        if (building.Floor is not null)
        {
            throw new InvalidOperationException("Building already has a(n) floor (1:1 relationship).");
        }

        await _repository.AddAsync(floor, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Floor floor, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(floor.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a building who already has another floor.
        if (existing.BuildingId != floor.BuildingId)
        {
            var Building = await _buildings.GetByIdAsync(floor.BuildingId, cancellationToken)
                ?? throw new InvalidOperationException("Building not found.");

            if (Building.Floor is not null && Building.Floor.Id != existing.Id)
            {
                throw new InvalidOperationException("Target building already has an floor (1:1 relationship).");
            }
        }

        existing.Name = floor.Name;
        existing.Level = floor.Level;

        existing.BuildingId = floor.BuildingId;
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
