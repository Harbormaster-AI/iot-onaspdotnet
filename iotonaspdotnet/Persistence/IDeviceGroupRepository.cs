using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface IDeviceGroupRepository
{
    Task<DeviceGroup?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DeviceGroup>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DeviceGroup deviceGroup, CancellationToken cancellationToken);
    Task UpdateAsync(DeviceGroup deviceGroup, CancellationToken cancellationToken);
    Task DeleteAsync(DeviceGroup deviceGroup, CancellationToken cancellationToken);
}
