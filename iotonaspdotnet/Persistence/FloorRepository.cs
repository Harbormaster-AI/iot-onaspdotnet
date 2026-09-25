
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class FloorRepository : IFloorRepository
{
    private readonly ApplicationDbContext _db;

    public FloorRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Floor?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Floors
            .Include(x => x.Building)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Floor>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Floors
            .AsNoTracking()
            .Include(x => x.Building)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Floor floor, CancellationToken cancellationToken)
    {
        _db.Floors.Add(floor);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Floor floor, CancellationToken cancellationToken)
    {
        _db.Floors.Update(floor);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Floor floor, CancellationToken cancellationToken)
    {
        _db.Floors.Remove(floor);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToRoomsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Rooms
            .Where(room =>
                request.ChildIds.Contains(room.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    room =>
                        EF.Property<Guid?>(
                            room,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromRoomsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Rooms
            .Where(room =>
                request.ChildIds.Contains(room.Id) &&
                EF.Property<Guid?>(
                    room,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    room =>
                        EF.Property<Guid?>(
                            room,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }

}
