using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface IAlertRuleRepository
{
    Task<AlertRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AlertRule>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AlertRule alertRule, CancellationToken cancellationToken);
    Task UpdateAsync(AlertRule alertRule, CancellationToken cancellationToken);
    Task DeleteAsync(AlertRule alertRule, CancellationToken cancellationToken);
}
