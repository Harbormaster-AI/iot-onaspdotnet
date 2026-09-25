using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface IMessagingEndpointRepository
{
    Task<MessagingEndpoint?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MessagingEndpoint>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(MessagingEndpoint messagingEndpoint, CancellationToken cancellationToken);
    Task UpdateAsync(MessagingEndpoint messagingEndpoint, CancellationToken cancellationToken);
    Task DeleteAsync(MessagingEndpoint messagingEndpoint, CancellationToken cancellationToken);

    Task AddToStreamsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromStreamsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
