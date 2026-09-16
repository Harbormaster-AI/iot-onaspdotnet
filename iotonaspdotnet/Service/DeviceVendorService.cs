using iotonaspdotnet.Domain
using iotonaspdotnet.Persistence

namespace iotonaspdotnet.Service

public interface IDeviceVendorService
{
    Task<DeviceVendor?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DeviceVendor>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(DeviceVendor deviceVendor, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(DeviceVendor deviceVendor, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class DeviceVendorService : IDeviceVendorService
{
    private readonly IDeviceVendorRepository _repository;

    public DeviceVendorService(
        IDeviceVendorRepository repository )
    {
        _repository = repository;
    }

    public Task<DeviceVendor?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<DeviceVendor>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(DeviceVendor deviceVendor, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(deviceVendor, cancellationToken);
    }

    public async Task<bool> UpdateAsync(DeviceVendor deviceVendor, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(deviceVendor.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a connectivityPlan who already has another deviceVendor.
        if (existing.ConnectivityPlanId != deviceVendor.ConnectivityPlanId)
        {
            var target;

        existing.attributeName = deviceVendor.attributeName;
        existing.attributeName = deviceVendor.attributeName;
        existing.attributeName = deviceVendor.attributeName;
        existing.attributeName = deviceVendor.attributeName;

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
