using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface IAlertRepository
{
    Task<Alert?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Alert>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Alert alert, CancellationToken cancellationToken);
    Task UpdateAsync(Alert alert, CancellationToken cancellationToken);
    Task DeleteAsync(Alert alert, CancellationToken cancellationToken);
}
