using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface IConnectivityPlanService {

    Task Create(ConnectivityPlan model , CancellationToken cancellationToken);
    Task<bool> Update(ConnectivityPlan model, CancellationToken cancellationToken);
    Task<ConnectivityPlan?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ConnectivityPlan>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignTenant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTenant(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToSimCards(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSimCards(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ConnectivityPlanService : IConnectivityPlanService
{
    private readonly IConnectivityPlanRepository _repository;

    public ConnectivityPlanService(
        IConnectivityPlanRepository repository )
    {
        _repository = repository;
    }


    public async Task Create(ConnectivityPlan model, CancellationToken cancellationToken)
    {

         await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(ConnectivityPlan model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = model.Name;
        existing.DataCapMB = model.DataCapMB;
        existing.BillingCycleDays = model.BillingCycleDays;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<ConnectivityPlan?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ConnectivityPlan>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignTenant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignTenant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToSimCards(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromSimCards(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
