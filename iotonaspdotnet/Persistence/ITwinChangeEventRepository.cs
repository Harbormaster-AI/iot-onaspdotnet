using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface ITwinChangeEventRepository
{
    Task<TwinChangeEvent?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TwinChangeEvent>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(TwinChangeEvent twinChangeEvent, CancellationToken cancellationToken);
    Task UpdateAsync(TwinChangeEvent twinChangeEvent, CancellationToken cancellationToken);
    Task DeleteAsync(TwinChangeEvent twinChangeEvent, CancellationToken cancellationToken);


}
