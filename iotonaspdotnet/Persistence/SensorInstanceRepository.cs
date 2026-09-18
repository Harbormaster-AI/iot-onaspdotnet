using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class SensorInstanceRepository : ISensorInstanceRepository
{
    private readonly ApplicationDbContext _db;

    public SensorInstanceRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<SensorInstance?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.SensorInstances
            .Include(x => x.Device)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<SensorInstance>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.SensorInstances
            .AsNoTracking()
            .Include(x => x.Device)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(SensorInstance sensorInstance, CancellationToken cancellationToken)
    {
        _db.SensorInstances.Add(sensorInstance);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SensorInstance sensorInstance, CancellationToken cancellationToken)
    {
        _db.SensorInstances.Update(sensorInstance);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SensorInstance sensorInstance, CancellationToken cancellationToken)
    {
        _db.SensorInstances.Remove(sensorInstance);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
