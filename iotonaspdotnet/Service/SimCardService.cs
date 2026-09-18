using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface ISimCardService
{
    Task<SimCard?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<SimCard>> GetAll(CancellationToken cancellationToken);
    Task Create(SimCardRequest request , CancellationToken cancellationToken);
    Task<bool> Update(SimCardRequest request, CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);


}

public class SimCardService : ISimCardService
{
    private readonly ISimCardRepository _repository;

    public SimCardService(
        ISimCardRepository repository )
    {
        _repository = repository;
    }

    public Task<SimCard?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<SimCard>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(SimCardRequest request, CancellationToken cancellationToken)
    {
        var tenant = await _tenants.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.Tenant is not null)
        {
            throw new InvalidOperationException("Tenant:Tenant already has a(n) SimCard (1:1 relationship).");
        }
        var connectivityPlan = await _connectivityPlans.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("ConnectivityPlan not found.");

        if (connectivityPlan.ConnectivityPlan is not null)
        {
            throw new InvalidOperationException("ConnectivityPlan:ConnectivityPlan already has a(n) SimCard (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(SimCardRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Iccid = request.Iccid;
        existing.Imsi = request.Imsi;
        existing.Carrier = request.Carrier;
        existing.NetworkProfiles = request.NetworkProfiles;
        existing.Tenant = request.Tenant;
        existing.ConnectivityPlan = request.ConnectivityPlan;
        existing.Status = request.Status;

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
