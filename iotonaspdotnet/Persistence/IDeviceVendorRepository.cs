using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Persistence;

public interface IDeviceVendorRepository
{
    Task<DeviceVendor?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DeviceVendor>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DeviceVendor deviceVendor, CancellationToken cancellationToken);
    Task UpdateAsync(DeviceVendor deviceVendor, CancellationToken cancellationToken);
    Task DeleteAsync(DeviceVendor deviceVendor, CancellationToken cancellationToken);

    Task AddToDeviceModelsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDeviceModelsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToFirmwareReleasesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromFirmwareReleasesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToHardwareModulesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromHardwareModulesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
