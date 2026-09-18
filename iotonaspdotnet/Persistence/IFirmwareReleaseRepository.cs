using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Persistence;

public interface IFirmwareReleaseRepository
{
    Task<FirmwareRelease?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<FirmwareRelease>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(FirmwareRelease firmwareRelease, CancellationToken cancellationToken);
    Task UpdateAsync(FirmwareRelease firmwareRelease, CancellationToken cancellationToken);
    Task DeleteAsync(FirmwareRelease firmwareRelease, CancellationToken cancellationToken);
}
