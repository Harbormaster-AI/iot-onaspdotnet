using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface IDigitalTwinRepository
{
    Task<DigitalTwin?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DigitalTwin>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DigitalTwin digitalTwin, CancellationToken cancellationToken);
    Task UpdateAsync(DigitalTwin digitalTwin, CancellationToken cancellationToken);
    Task DeleteAsync(DigitalTwin digitalTwin, CancellationToken cancellationToken);

    Task AddToChangeEventsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromChangeEventsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
