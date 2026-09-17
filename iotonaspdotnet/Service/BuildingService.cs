using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IBuildingService
{
    Task<Building?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Building>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(Building building, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Building building, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class BuildingService : IBuildingService
{
    private readonly IBuildingRepository _repository;
    private readonly ISiteRepository _sites;

    public BuildingService(
        ISiteRepository sites,
        IBuildingRepository repository )
    {
        _repository = repository;
        _sites = sites;
    }

    public Task<Building?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<Building>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(Building building, CancellationToken cancellationToken)
    {
        var site.Site = await _sites.GetByIdAsync(building.Id, cancellationToken)
            ?? throw new InvalidOperationException("Site not found.");

        if (site.Site is not null)
        {
            throw new InvalidOperationException("Site already has a(n) building (1:1 relationship).");
        }
        await _repository.AddAsync(building, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Building building, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(building.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a site who already has another building.
        if (existing.Id != building.Id)
        {
            var Site = await _sites.GetByIdAsync(building.Id, cancellationToken)
                ?? throw new InvalidOperationException("Site not found.");

            if (Site.Building is not null && Site.Building.Id != existing.Id)
            {
                throw new InvalidOperationException("Target site already has an building (1:1 relationship).");
            }
        }

        existing.Name = building.Name;

        existing.Id = building.Id;
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
