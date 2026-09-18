using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface ITenantService
{
    Task<Tenant?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Tenant>> GetAll(CancellationToken cancellationToken);
    Task Create(TenantRequest request , CancellationToken cancellationToken);
    Task<bool> Update(TenantRequest request, CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);


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

    public async Task<bool> DeleteAsync(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }


}
