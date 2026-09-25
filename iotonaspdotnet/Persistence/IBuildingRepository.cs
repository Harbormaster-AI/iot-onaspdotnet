using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface IBuildingRepository
{
    Task<Building?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Building>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Building building, CancellationToken cancellationToken);
    Task UpdateAsync(Building building, CancellationToken cancellationToken);
    Task DeleteAsync(Building building, CancellationToken cancellationToken);

    Task AddToFloorsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromFloorsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
