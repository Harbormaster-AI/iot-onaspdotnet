using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface IAlertRuleRepository
{
    Task<AlertRule?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AlertRule>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AlertRule alertRule, CancellationToken cancellationToken);
    Task UpdateAsync(AlertRule alertRule, CancellationToken cancellationToken);
    Task DeleteAsync(AlertRule alertRule, CancellationToken cancellationToken);

    Task AddToStreamsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromStreamsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAlertsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAlertsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
