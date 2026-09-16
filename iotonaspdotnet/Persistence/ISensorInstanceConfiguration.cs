using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface ISensorInstanceRepository
{
    Task<SensorInstance?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<SensorInstance>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(SensorInstance sensorInstance, CancellationToken cancellationToken);
    Task UpdateAsync(SensorInstance sensorInstance, CancellationToken cancellationToken);
    Task DeleteAsync(SensorInstance sensorInstance, CancellationToken cancellationToken);
}
