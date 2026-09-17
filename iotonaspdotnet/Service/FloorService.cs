using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IFloorService
{
    Task<Floor?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Floor>> GetAll(CancellationToken cancellationToken);
    Task Create(FloorRequest request , CancellationToken cancellationToken);
    Task<bool> Update(FloorRequest request, CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);


}

public class FloorService : IFloorService
{
    private readonly IFloorRepository _repository;

    public FloorService(
        IFloorRepository repository )
    {
        _repository = repository;
    }

    public Task<Floor?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<Floor>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(FloorRequest request, CancellationToken cancellationToken)
    {
        var building = await _buildings.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Building not found.");

        if (building.Building is not null)
        {
            throw new InvalidOperationException("Building:Building already has a(n) Floor (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(FloorRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = request.Name;
        existing.Level = request.Level;
        existing.Building = request.Building;
        existing.Rooms = request.Rooms;
        await _repository.UpdateAsync(existing, cancellationToken);
    }

        existing.Name = floor.Name;
        existing.Level = floor.Level;

        existing.Id = floor.Id;
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
