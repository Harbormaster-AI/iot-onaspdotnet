using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface IUsageRecordRepository
{
    Task<UsageRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<UsageRecord>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(UsageRecord usageRecord, CancellationToken cancellationToken);
    Task UpdateAsync(UsageRecord usageRecord, CancellationToken cancellationToken);
    Task DeleteAsync(UsageRecord usageRecord, CancellationToken cancellationToken);


}
