using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface ISimCardService
{
    Task<SimCard?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<SimCard>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(SimCard simCard, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(SimCard simCard, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class SimCardService : ISimCardService
{
    private readonly ISimCardRepository _repository;
    private readonly ITenantRepository _tenants;
    private readonly IConnectivityPlanRepository _connectivityPlans;

    public SimCardService(
        ITenantRepository tenants,
        IConnectivityPlanRepository connectivityPlans,
        ISimCardRepository repository )
    {
        _repository = repository;
        _tenants = tenants;
        _connectivityPlans = connectivityPlans;
    }

    public Task<SimCard?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<SimCard>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(SimCard simCard, CancellationToken cancellationToken)
    {
        var tenant = await _tenants.GetByIdAsync(simCard.Id, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.Tenant is not null)
        {
            throw new InvalidOperationException("Tenant already has a(n) simCard (1:1 relationship).");
        }
        var connectivityPlan = await _connectivityPlans.GetByIdAsync(simCard.Id, cancellationToken)
            ?? throw new InvalidOperationException("ConnectivityPlan not found.");

        if (connectivityPlan.ConnectivityPlan is not null)
        {
            throw new InvalidOperationException("ConnectivityPlan already has a(n) simCard (1:1 relationship).");
        }
        await _repository.AddAsync(simCard, cancellationToken);
    }

    public async Task<bool> UpdateAsync(SimCard simCard, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(simCard.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a connectivityPlan who already has another simCard.
        if (existing.Id != simCard.Id)
        {
            var Tenant = await _tenants.GetByIdAsync(simCard.Id, cancellationToken)
                ?? throw new InvalidOperationException("Tenant not found.");

            if (Tenant.SimCard is not null && Tenant.SimCard.Id != existing.Id)
            {
                throw new InvalidOperationException("Target tenant already has an simCard (1:1 relationship).");
            }
            var ConnectivityPlan = await _connectivityPlans.GetByIdAsync(simCard.Id, cancellationToken)
                ?? throw new InvalidOperationException("ConnectivityPlan not found.");

            if (ConnectivityPlan.SimCard is not null && ConnectivityPlan.SimCard.Id != existing.Id)
            {
                throw new InvalidOperationException("Target connectivityPlan already has an simCard (1:1 relationship).");
            }
        }

        existing.Iccid = simCard.Iccid;
        existing.Imsi = simCard.Imsi;
        existing.Carrier = simCard.Carrier;
        existing.Status = simCard.Status;

        existing.Id = simCard.Id;
        existing.Id = simCard.Id;
        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }
}
