using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface IActuatorInstanceRepository
{
    Task<ActuatorInstance?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ActuatorInstance>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ActuatorInstance actuatorInstance, CancellationToken cancellationToken);
    Task UpdateAsync(ActuatorInstance actuatorInstance, CancellationToken cancellationToken);
    Task DeleteAsync(ActuatorInstance actuatorInstance, CancellationToken cancellationToken);
}
