using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class TelemetryStreamRepository : ITelemetryStreamRepository
{
    private readonly ApplicationDbContext _db;

    public TelemetryStreamRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<TelemetryStream?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.TelemetryStreams
            .Include(x => x.Device)
            .Include(x => x.Sensor)
            .Include(x => x.Schema)
            .Include(x => x.MessagingEndpoint)
            .Include(x => x.RetentionPolicy)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<TelemetryStream>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.TelemetryStreams
            .AsNoTracking()
            .Include(x => x.${$roleName})
            .Include(x => x.${$roleName})
            .Include(x => x.${$roleName})
            .Include(x => x.${$roleName})
            .Include(x => x.${$roleName})
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TelemetryStream telemetryStream, CancellationToken cancellationToken)
    {
        _db.TelemetryStreams.Add(telemetryStream);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TelemetryStream telemetryStream, CancellationToken cancellationToken)
    {
        _db.TelemetryStreams.Update(telemetryStream);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TelemetryStream telemetryStream, CancellationToken cancellationToken)
    {
        _db.TelemetryStreams.Remove(telemetryStream);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
