using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IDeviceGroupService
{
    Task<DeviceGroup?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<DeviceGroup>> GetAll(CancellationToken cancellationToken);
    Task Create(DeviceGroupRequest request , CancellationToken cancellationToken);
    Task<bool> Update(DeviceGroupRequest request, CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);


}

public class DeviceGroupService : IDeviceGroupService
{
    private readonly IDeviceGroupRepository _repository;

    public DeviceGroupService(
        IDeviceGroupRepository repository )
    {
        _repository = repository;
    }

    public Task<DeviceGroup?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<DeviceGroup>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(DeviceGroupRequest request, CancellationToken cancellationToken)
    {
        var tenant = await _tenants.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.Tenant is not null)
        {
            throw new InvalidOperationException("Tenant:Tenant already has a(n) DeviceGroup (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(DeviceGroupRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = request.Name;
        existing.Criteria = request.Criteria;
        existing.Tenant = request.Tenant;
        existing.Devices = request.Devices;
        await _repository.UpdateAsync(existing, cancellationToken);
    }

        existing.Name = deviceGroup.Name;
        existing.Criteria = deviceGroup.Criteria;

        existing.Id = deviceGroup.Id;
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
