using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IConnectivityPlanService
{
    Task<ConnectivityPlan?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ConnectivityPlan>> GetAll(CancellationToken cancellationToken);
    Task Create(ConnectivityPlanRequest request , CancellationToken cancellationToken);
    Task<bool> Update(ConnectivityPlanRequest request, CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);


}

public class ConnectivityPlanService : IConnectivityPlanService
{
    private readonly IConnectivityPlanRepository _repository;

    public ConnectivityPlanService(
        IConnectivityPlanRepository repository )
    {
        _repository = repository;
    }

    public Task<ConnectivityPlan?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<ConnectivityPlan>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(ConnectivityPlanRequest request, CancellationToken cancellationToken)
    {
        var tenant = await _tenants.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.Tenant is not null)
        {
            throw new InvalidOperationException("Tenant:Tenant already has a(n) ConnectivityPlan (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(ConnectivityPlanRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = request.Name;
        existing.DataCapMB = request.DataCapMB;
        existing.BillingCycleDays = request.BillingCycleDays;
        existing.SimCards = request.SimCards;
        existing.Tenant = request.Tenant;

        existing.Name = connectivityPlan.Name;
        existing.DataCapMB = connectivityPlan.DataCapMB;
        existing.BillingCycleDays = connectivityPlan.BillingCycleDays;

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
