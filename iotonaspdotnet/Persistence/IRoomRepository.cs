using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface IRoomRepository
{
    Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Room>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Room room, CancellationToken cancellationToken);
    Task UpdateAsync(Room room, CancellationToken cancellationToken);
    Task DeleteAsync(Room room, CancellationToken cancellationToken);
}
