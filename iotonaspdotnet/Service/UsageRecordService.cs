using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IUsageRecordService
{
    Task<UsageRecord?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<UsageRecord>> GetAll(CancellationToken cancellationToken);
    Task Create(UsageRecordRequest request , CancellationToken cancellationToken);
    Task<bool> Update(UsageRecordRequest request, CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);


}

public class UsageRecordService : IUsageRecordService
{
    private readonly IUsageRecordRepository _repository;

    public UsageRecordService(
        IUsageRecordRepository repository )
    {
        _repository = repository;
    }

    public Task<UsageRecord?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<UsageRecord>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(UsageRecordRequest request, CancellationToken cancellationToken)
    {
        var tenant = await _tenants.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.Tenant is not null)
        {
            throw new InvalidOperationException("Tenant:Tenant already has a(n) UsageRecord (1:1 relationship).");
        }
        var ioTDevice = await _ioTDevices.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.Device is not null)
        {
            throw new InvalidOperationException("IoTDevice:Device already has a(n) UsageRecord (1:1 relationship).");
        }
        var connectivityPlan = await _connectivityPlans.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("ConnectivityPlan not found.");

        if (connectivityPlan.ConnectivityPlan is not null)
        {
            throw new InvalidOperationException("ConnectivityPlan:ConnectivityPlan already has a(n) UsageRecord (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(UsageRecordRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.PeriodStart = request.PeriodStart;
        existing.PeriodEnd = request.PeriodEnd;
        existing.MessagesSent = request.MessagesSent;
        existing.DataVolumeMB = request.DataVolumeMB;
        existing.Tenant = request.Tenant;
        existing.Device = request.Device;
        existing.ConnectivityPlan = request.ConnectivityPlan;

        existing.PeriodStart = usageRecord.PeriodStart;
        existing.PeriodEnd = usageRecord.PeriodEnd;
        existing.MessagesSent = usageRecord.MessagesSent;
        existing.DataVolumeMB = usageRecord.DataVolumeMB;

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
