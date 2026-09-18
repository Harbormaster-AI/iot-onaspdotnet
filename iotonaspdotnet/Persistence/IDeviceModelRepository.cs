using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface IDeviceModelRepository
{
    Task<DeviceModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DeviceModel>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DeviceModel deviceModel, CancellationToken cancellationToken);
    Task UpdateAsync(DeviceModel deviceModel, CancellationToken cancellationToken);
    Task DeleteAsync(DeviceModel deviceModel, CancellationToken cancellationToken);
}
