using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface ITelemetrySchemaRepository
{
    Task<TelemetrySchema?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TelemetrySchema>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(TelemetrySchema telemetrySchema, CancellationToken cancellationToken);
    Task UpdateAsync(TelemetrySchema telemetrySchema, CancellationToken cancellationToken);
    Task DeleteAsync(TelemetrySchema telemetrySchema, CancellationToken cancellationToken);
}
