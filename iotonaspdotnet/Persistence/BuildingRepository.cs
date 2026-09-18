using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class BuildingRepository : IBuildingRepository
{
    private readonly ApplicationDbContext _db;

    public BuildingRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Building?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Buildings
            .Include(x => x.Site)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Building>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Buildings
            .AsNoTracking()
            .Include(x => x.${$roleName})
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Building building, CancellationToken cancellationToken)
    {
        _db.Buildings.Add(building);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Building building, CancellationToken cancellationToken)
    {
        _db.Buildings.Update(building);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Building building, CancellationToken cancellationToken)
    {
        _db.Buildings.Remove(building);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
