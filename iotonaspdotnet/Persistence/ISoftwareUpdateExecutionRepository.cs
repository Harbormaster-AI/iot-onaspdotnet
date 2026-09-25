using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface ISoftwareUpdateExecutionRepository
{
    Task<SoftwareUpdateExecution?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<SoftwareUpdateExecution>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(SoftwareUpdateExecution softwareUpdateExecution, CancellationToken cancellationToken);
    Task UpdateAsync(SoftwareUpdateExecution softwareUpdateExecution, CancellationToken cancellationToken);
    Task DeleteAsync(SoftwareUpdateExecution softwareUpdateExecution, CancellationToken cancellationToken);


}
