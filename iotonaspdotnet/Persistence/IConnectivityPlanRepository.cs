using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface IConnectivityPlanRepository
{
    Task<ConnectivityPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ConnectivityPlan>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ConnectivityPlan connectivityPlan, CancellationToken cancellationToken);
    Task UpdateAsync(ConnectivityPlan connectivityPlan, CancellationToken cancellationToken);
    Task DeleteAsync(ConnectivityPlan connectivityPlan, CancellationToken cancellationToken);
}
