using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface ISiteRepository
{
    Task<Site?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Site>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Site site, CancellationToken cancellationToken);
    Task UpdateAsync(Site site, CancellationToken cancellationToken);
    Task DeleteAsync(Site site, CancellationToken cancellationToken);

    Task AddToBuildingsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromBuildingsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDevicesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDevicesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToGatewaysAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromGatewaysAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
