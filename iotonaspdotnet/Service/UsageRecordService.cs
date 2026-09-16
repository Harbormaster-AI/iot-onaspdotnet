using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Persistence.Tenants;
using iotonaspdotnet.Persistence.IoTDevices;
using iotonaspdotnet.Persistence.ConnectivityPlans;

namespace iotonaspdotnet.Service;

public interface IUsageRecordService
{
    Task<UsageRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<UsageRecord>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(UsageRecord usageRecord, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(UsageRecord usageRecord, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class UsageRecordService : IUsageRecordService
{
    private readonly IUsageRecordRepository _repository;
    private readonly ITenantRepository _tenants;
    private readonly IIoTDeviceRepository _ioTDevices;
    private readonly IConnectivityPlanRepository _connectivityPlans;

    public UsageRecordService(
        ITenantRepository tenants,
        IIoTDeviceRepository ioTDevices,
        IConnectivityPlanRepository connectivityPlans,
        IUsageRecordRepository repository )
    {
        _repository = repository;
        _connectivityPlans = connectivityPlans;
        _connectivityPlans = connectivityPlans;
        _connectivityPlans = connectivityPlans;
    }

    public Task<UsageRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<UsageRecord>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(UsageRecord usageRecord, CancellationToken cancellationToken)
    {
        var tenant = await _tenants.GetByIdAsync(usageRecord.TenantId, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.UsageRecord is not null)
        {
            throw new InvalidOperationException("Tenant already has a(n) usageRecord (1:1 relationship).");
        }

        var ioTDevice = await _ioTDevices.GetByIdAsync(usageRecord.IoTDeviceId, cancellationToken)
            ?? throw new InvalidOperationException("IoTDevice not found.");

        if (ioTDevice.UsageRecord is not null)
        {
            throw new InvalidOperationException("IoTDevice already has a(n) usageRecord (1:1 relationship).");
        }

        var connectivityPlan = await _connectivityPlans.GetByIdAsync(usageRecord.ConnectivityPlanId, cancellationToken)
            ?? throw new InvalidOperationException("ConnectivityPlan not found.");

        if (connectivityPlan.UsageRecord is not null)
        {
            throw new InvalidOperationException("ConnectivityPlan already has a(n) usageRecord (1:1 relationship).");
        }

        await _repository.AddAsync(usageRecord, cancellationToken);
    }

    public async Task<bool> UpdateAsync(UsageRecord usageRecord, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(usageRecord.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a connectivityPlan who already has another usageRecord.
        if (existing.ConnectivityPlanId != usageRecord.ConnectivityPlanId)
        {
            var Tenant = await _tenants.GetByIdAsync(usageRecord.TenantId, cancellationToken)
                ?? throw new InvalidOperationException("Tenant not found.");

            if (Tenant.UsageRecord is not null && Tenant.UsageRecord.Id != existing.Id)
            {
                throw new InvalidOperationException("Target tenant already has an usageRecord (1:1 relationship).");
            }
            var Device = await _ioTDevices.GetByIdAsync(usageRecord.IoTDeviceId, cancellationToken)
                ?? throw new InvalidOperationException("IoTDevice not found.");

            if (Device.UsageRecord is not null && Device.UsageRecord.Id != existing.Id)
            {
                throw new InvalidOperationException("Target ioTDevice already has an usageRecord (1:1 relationship).");
            }
            var ConnectivityPlan = await _connectivityPlans.GetByIdAsync(usageRecord.ConnectivityPlanId, cancellationToken)
                ?? throw new InvalidOperationException("ConnectivityPlan not found.");

            if (ConnectivityPlan.UsageRecord is not null && ConnectivityPlan.UsageRecord.Id != existing.Id)
            {
                throw new InvalidOperationException("Target connectivityPlan already has an usageRecord (1:1 relationship).");
            }
        }

        existing.PeriodStart = usageRecord.PeriodStart;
        existing.PeriodEnd = usageRecord.PeriodEnd;
        existing.MessagesSent = usageRecord.MessagesSent;
        existing.DataVolumeMB = usageRecord.DataVolumeMB;

        existing.TenantId = usageRecord.TenantId;
        existing.IoTDeviceId = usageRecord.IoTDeviceId;
        existing.ConnectivityPlanId = usageRecord.ConnectivityPlanId;
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
