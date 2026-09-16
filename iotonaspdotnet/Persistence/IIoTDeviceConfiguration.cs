using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface IIoTDeviceRepository
{
    Task<IoTDevice?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<IoTDevice>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(IoTDevice ioTDevice, CancellationToken cancellationToken);
    Task UpdateAsync(IoTDevice ioTDevice, CancellationToken cancellationToken);
    Task DeleteAsync(IoTDevice ioTDevice, CancellationToken cancellationToken);
}
