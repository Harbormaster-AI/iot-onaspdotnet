using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class DigitalTwinRepository : IDigitalTwinRepository
{
    private readonly ApplicationDbContext _db;

    public DigitalTwinRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<DigitalTwin?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.DigitalTwins
            .Include(x => x.IoTDevice)
            .Include(x => x.Gateway)
            .Include(x => x.TwinTemplate)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<DigitalTwin>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.DigitalTwins
            .AsNoTracking()
            .Include(x => x.IoTDevice)
            .Include(x => x.Gateway)
            .Include(x => x.TwinTemplate)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(DigitalTwin digitalTwin, CancellationToken cancellationToken)
    {
        _db.DigitalTwins.Add(digitalTwin);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task
    (DigitalTwin digitalTwin, CancellationToken cancellationToken)
    {
        _db.DigitalTwins.Update(digitalTwin);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DigitalTwin digitalTwin, CancellationToken cancellationToken)
    {
        _db.DigitalTwins.Remove(digitalTwin);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
