using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface IFloorRepository
{
    Task<Floor?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Floor>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Floor floor, CancellationToken cancellationToken);
    Task UpdateAsync(Floor floor, CancellationToken cancellationToken);
    Task DeleteAsync(Floor floor, CancellationToken cancellationToken);
}
