
using iotonaspdotnet.Contracts;
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


    public async Task AddToStreamsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TelemetryStreams
            .Where(telemetryStream =>
                request.ChildIds.Contains(telemetryStream.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    telemetryStream =>
                        EF.Property<Guid?>(
                            telemetryStream,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromStreamsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TelemetryStreams
            .Where(telemetryStream =>
                request.ChildIds.Contains(telemetryStream.Id) &&
                EF.Property<Guid?>(
                    telemetryStream,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    telemetryStream =>
                        EF.Property<Guid?>(
                            telemetryStream,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }

}
