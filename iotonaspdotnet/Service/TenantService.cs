using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface ITenantService {

    Task Create(TenantRequest request , CancellationToken cancellationToken);
    Task<bool> Update(TenantRequest request, CancellationToken cancellationToken);
    Task<Tenant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Tenant>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToSites(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSites(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToUsers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromUsers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDevices(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDevices(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDataRetentionPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDataRetentionPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToConnectivityPlans(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromConnectivityPlans(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToSimCards(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSimCards(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToMessagingEndpoints(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromMessagingEndpoints(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAccessPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAccessPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDeviceGroups(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDeviceGroups(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAlertRules(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAlertRules(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToMaintenanceTickets(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromMaintenanceTickets(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToUsageRecords(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromUsageRecords(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class TenantService : ITenantService
{
    private readonly ITenantRepository _repository;

    public TenantService(
        ITenantRepository repository )
    {
        _repository = repository;
    }

    public Task<Tenant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<Tenant>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(TenantRequest request, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(TenantRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = request.Name;
        existing.Sites = request.Sites;
        existing.Users = request.Users;
        existing.Devices = request.Devices;
        existing.DataRetentionPolicies = request.DataRetentionPolicies;
        existing.ConnectivityPlans = request.ConnectivityPlans;
        existing.SimCards = request.SimCards;
        existing.MessagingEndpoints = request.MessagingEndpoints;
        existing.AccessPolicies = request.AccessPolicies;
        existing.DeviceGroups = request.DeviceGroups;
        existing.AlertRules = request.AlertRules;
        existing.MaintenanceTickets = request.MaintenanceTickets;
        existing.UsageRecords = request.UsageRecords;
        existing.TenantType = request.TenantType;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }


    Task<bool> AddToSites(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromSites(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AddToUsers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromUsers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AddToDevices(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromDevices(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AddToDataRetentionPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromDataRetentionPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AddToConnectivityPlans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromConnectivityPlans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AddToSimCards(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromSimCards(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AddToMessagingEndpoints(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromMessagingEndpoints(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AddToAccessPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromAccessPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AddToDeviceGroups(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromDeviceGroups(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AddToAlertRules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromAlertRules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AddToMaintenanceTickets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromMaintenanceTickets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    Task<bool> AddToUsageRecords(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    Task<bool> RemoveFromUsageRecords(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
