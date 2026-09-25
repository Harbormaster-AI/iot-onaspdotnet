using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface IDeviceModelRepository
{
    Task<DeviceModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DeviceModel>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DeviceModel deviceModel, CancellationToken cancellationToken);
    Task UpdateAsync(DeviceModel deviceModel, CancellationToken cancellationToken);
    Task DeleteAsync(DeviceModel deviceModel, CancellationToken cancellationToken);

    Task AddToHardwareModulesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromHardwareModulesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToFirmwareReleasesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromFirmwareReleasesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCommandDefinitionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCommandDefinitionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
