using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface ITenantService {

    Task Create(Tenant model , CancellationToken cancellationToken);
    Task<bool> Update(Tenant model, CancellationToken cancellationToken);
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


    public async Task Create(Tenant model, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(Tenant model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = model.Name;
        existing.TenantType = model.TenantType;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<Tenant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Tenant>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

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


    public async Task<bool> AddToSites(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromSites(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToUsers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromUsers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToDevices(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDevices(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToDataRetentionPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDataRetentionPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToConnectivityPlans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromConnectivityPlans(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToSimCards(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromSimCards(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToMessagingEndpoints(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromMessagingEndpoints(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToAccessPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAccessPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToDeviceGroups(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDeviceGroups(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToAlertRules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAlertRules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToMaintenanceTickets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromMaintenanceTickets(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToUsageRecords(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromUsageRecords(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
