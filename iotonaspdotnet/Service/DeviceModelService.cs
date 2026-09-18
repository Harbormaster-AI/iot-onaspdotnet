using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface IDeviceModelService {

    Task Create(DeviceModel model , CancellationToken cancellationToken);
    Task<bool> Update(DeviceModel model, CancellationToken cancellationToken);
    Task<DeviceModel?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<DeviceModel>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignVendor(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignVendor(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignTwinTemplate(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTwinTemplate(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToHardwareModules(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromHardwareModules(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToFirmwareReleases(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromFirmwareReleases(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCommandDefinitions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCommandDefinitions(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class DeviceModelService : IDeviceModelService
{
    private readonly IDeviceModelRepository _repository;

    public DeviceModelService(
        IDeviceModelRepository repository )
    {
        _repository = repository;
    }


    public async Task Create(DeviceModel model, CancellationToken cancellationToken)
    {

 
         await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(DeviceModel model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.Name = model.Name;
        existing.ModelNumber = model.ModelNumber;
        existing.HardwareRevision = model.HardwareRevision;
        existing.SupportedConnectivity = model.SupportedConnectivity;
        existing.DefaultTelemetryEncoding = model.DefaultTelemetryEncoding;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<DeviceModel?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<DeviceModel>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> AssignVendor(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignVendor(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignTwinTemplate(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignTwinTemplate(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToHardwareModules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromHardwareModules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToFirmwareReleases(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromFirmwareReleases(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToCommandDefinitions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCommandDefinitions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
