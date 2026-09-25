using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface IRoomRepository
{
    Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Room>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Room room, CancellationToken cancellationToken);
    Task UpdateAsync(Room room, CancellationToken cancellationToken);
    Task DeleteAsync(Room room, CancellationToken cancellationToken);

    Task AddToDevicesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDevicesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToGatewaysAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromGatewaysAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
