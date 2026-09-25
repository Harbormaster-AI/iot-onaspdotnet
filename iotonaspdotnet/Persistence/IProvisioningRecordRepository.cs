using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface IProvisioningRecordRepository
{
    Task<ProvisioningRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ProvisioningRecord>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ProvisioningRecord provisioningRecord, CancellationToken cancellationToken);
    Task UpdateAsync(ProvisioningRecord provisioningRecord, CancellationToken cancellationToken);
    Task DeleteAsync(ProvisioningRecord provisioningRecord, CancellationToken cancellationToken);


}
