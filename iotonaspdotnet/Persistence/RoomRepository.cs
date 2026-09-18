using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _db;

    public RoomRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Rooms
            .Include(x => x.Floor)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Room>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Rooms
            .AsNoTracking()
            .Include(x => x.Floor)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Room room, CancellationToken cancellationToken)
    {
        _db.Rooms.Add(room);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task
    (Room room, CancellationToken cancellationToken)
    {
        _db.Rooms.Update(room);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Room room, CancellationToken cancellationToken)
    {
        _db.Rooms.Remove(room);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
