using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface IFloorRepository
{
    Task<Floor?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Floor>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Floor floor, CancellationToken cancellationToken);
    Task UpdateAsync(Floor floor, CancellationToken cancellationToken);
    Task DeleteAsync(Floor floor, CancellationToken cancellationToken);

    Task AddToRoomsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRoomsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
