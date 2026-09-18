using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface ITwinTemplateRepository
{
    Task<TwinTemplate?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TwinTemplate>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(TwinTemplate twinTemplate, CancellationToken cancellationToken);
    Task UpdateAsync(TwinTemplate twinTemplate, CancellationToken cancellationToken);
    Task DeleteAsync(TwinTemplate twinTemplate, CancellationToken cancellationToken);
}
