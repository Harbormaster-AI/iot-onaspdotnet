using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface ITenantRepository
{
    Task<Tenant?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Tenant>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Tenant tenant, CancellationToken cancellationToken);
    Task UpdateAsync(Tenant tenant, CancellationToken cancellationToken);
    Task DeleteAsync(Tenant tenant, CancellationToken cancellationToken);

    Task AddToSitesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSitesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToUsersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromUsersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDevicesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDevicesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDataRetentionPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDataRetentionPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToConnectivityPlansAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromConnectivityPlansAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToSimCardsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSimCardsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToMessagingEndpointsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromMessagingEndpointsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAccessPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAccessPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDeviceGroupsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDeviceGroupsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAlertRulesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAlertRulesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToMaintenanceTicketsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromMaintenanceTicketsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToUsageRecordsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromUsageRecordsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
