using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface IDeviceVendorRepository
{
    Task<DeviceVendor?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DeviceVendor>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DeviceVendor deviceVendor, CancellationToken cancellationToken);
    Task UpdateAsync(DeviceVendor deviceVendor, CancellationToken cancellationToken);
    Task DeleteAsync(DeviceVendor deviceVendor, CancellationToken cancellationToken);
}
