using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class TwinChangeEventRepository : ITwinChangeEventRepository
{
    private readonly ApplicationDbContext _db;

    public TwinChangeEventRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<TwinChangeEvent?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.TwinChangeEvents
            .Include(x => x.DigitalTwin)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<TwinChangeEvent>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.TwinChangeEvents
            .AsNoTracking()
            .Include(x => x.DigitalTwin)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TwinChangeEvent twinChangeEvent, CancellationToken cancellationToken)
    {
        _db.TwinChangeEvents.Add(twinChangeEvent);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TwinChangeEvent twinChangeEvent, CancellationToken cancellationToken)
    {
        _db.TwinChangeEvents.Update(twinChangeEvent);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TwinChangeEvent twinChangeEvent, CancellationToken cancellationToken)
    {
        _db.TwinChangeEvents.Remove(twinChangeEvent);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
