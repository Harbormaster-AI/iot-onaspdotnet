using iotonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class TelemetrySchemaRepository : ITelemetrySchemaRepository
{
    private readonly ApplicationDbContext _db;

    public TelemetrySchemaRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<TelemetrySchema?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.TelemetrySchemas
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<TelemetrySchema>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.TelemetrySchemas
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TelemetrySchema telemetrySchema, CancellationToken cancellationToken)
    {
        _db.TelemetrySchemas.Add(telemetrySchema);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TelemetrySchema telemetrySchema, CancellationToken cancellationToken)
    {
        _db.TelemetrySchemas.Update(telemetrySchema);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TelemetrySchema telemetrySchema, CancellationToken cancellationToken)
    {
        _db.TelemetrySchemas.Remove(telemetrySchema);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
