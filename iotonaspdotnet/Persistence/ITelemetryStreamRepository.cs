using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface ITelemetryStreamRepository
{
    Task<TelemetryStream?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TelemetryStream>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(TelemetryStream telemetryStream, CancellationToken cancellationToken);
    Task UpdateAsync(TelemetryStream telemetryStream, CancellationToken cancellationToken);
    Task DeleteAsync(TelemetryStream telemetryStream, CancellationToken cancellationToken);


}
