using iotonaspdotnet.Domain
using iotonaspdotnet.Persistence
using iotonaspdotnet.Persistence.Tenants;

namespace iotonaspdotnet.Service

public interface IDeviceGroupService
{
    Task<DeviceGroup?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DeviceGroup>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(DeviceGroup deviceGroup, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(DeviceGroup deviceGroup, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class DeviceGroupService : IDeviceGroupService
{
    private readonly IDeviceGroupRepository _repository;
    private readonly ITenantRepository _tenants;

    public DeviceGroupService(
        ITenantRepository tenants,
        IDeviceGroupRepository repository )
    {
        _repository = repository;
        _tenants = tenants;
    }

    public Task<DeviceGroup?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<DeviceGroup>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(DeviceGroup deviceGroup, CancellationToken cancellationToken)
    {
        var tenant = await _tenants.GetByIdAsync(deviceGroup.TenantId, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.DeviceGroup is not null)
        {
            throw new InvalidOperationException("Tenant already has a(n) deviceGroup (1:1 relationship).");
        }

        await _repository.AddAsync(deviceGroup, cancellationToken);
    }

    public async Task<bool> UpdateAsync(DeviceGroup deviceGroup, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(deviceGroup.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a tenant who already has another deviceGroup.
        if (existing.TenantId != deviceGroup.TenantId)
        {
            var target;
            target = await _tenants.GetByIdAsync(deviceGroup.TenantId, cancellationToken)
                ?? throw new InvalidOperationException("Tenant not found.");

            if (target.DeviceGroup is not null && target.DeviceGroup.Id != existing.Id)
            {
                throw new InvalidOperationException("Target tenant already has an deviceGroup (1:1 relationship).");
            }

        }

        existing.attributeName = deviceGroup.attributeName;
        existing.attributeName = deviceGroup.attributeName;

        existing.TenantId = deviceGroup.TenantId;
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
