using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface IUsageRecordService {

    Task Create(UsageRecord model , CancellationToken cancellationToken);
    Task<bool> Update(UsageRecord model, CancellationToken cancellationToken);
    Task<UsageRecord?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<UsageRecord>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignTenant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTenant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignConnectivityPlan(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignConnectivityPlan(AssociationRequest request, CancellationToken cancellationToken);


}

public class UsageRecordService : IUsageRecordService
{
    private readonly IUsageRecordRepository _repository;

    public UsageRecordService(
        IUsageRecordRepository repository )
    {
        _repository = repository;
    }


    public async Task Create(UsageRecord model, CancellationToken cancellationToken)
    {

 
 
         await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(UsageRecord model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.PeriodStart = model.PeriodStart;
        existing.PeriodEnd = model.PeriodEnd;
        existing.MessagesSent = model.MessagesSent;
        existing.DataVolumeMB = model.DataVolumeMB;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<UsageRecord?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<UsageRecord>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDevice(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignConnectivityPlan(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignConnectivityPlan(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
