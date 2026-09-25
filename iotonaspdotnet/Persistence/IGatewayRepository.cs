using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface IGatewayRepository
{
    Task<Gateway?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Gateway>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Gateway gateway, CancellationToken cancellationToken);
    Task UpdateAsync(Gateway gateway, CancellationToken cancellationToken);
    Task DeleteAsync(Gateway gateway, CancellationToken cancellationToken);

    Task AddToDevicesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDevicesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToEdgeApplicationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromEdgeApplicationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCertificatesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCertificatesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToNetworkProfilesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromNetworkProfilesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
