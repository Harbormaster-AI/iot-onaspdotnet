using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Service;

public interface IIoTDeviceService {

    Task Create(IoTDevice model , CancellationToken cancellationToken);
    Task<bool> Update(IoTDevice model, CancellationToken cancellationToken);
    Task<IoTDevice?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<IoTDevice>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignDeviceModel(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDeviceModel(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignTenant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignTenant(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignSite(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignSite(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignRoom(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRoom(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignGateway(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignGateway(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignDigitalTwin(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignDigitalTwin(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignProvisioningRecord(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignProvisioningRecord(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToSensors(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSensors(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToActuators(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromActuators(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCertificates(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCertificates(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToTelemetryStreams(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTelemetryStreams(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCommandInvocations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCommandInvocations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAlerts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAlerts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDeviceGroups(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDeviceGroups(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToNetworkProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromNetworkProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class IoTDeviceService : IIoTDeviceService
{
    private readonly IIoTDeviceRepository _repository;

    public IoTDeviceService(
        IIoTDeviceRepository repository )
    {
        _repository = repository;
    }


    public async Task Create(IoTDevice model, CancellationToken cancellationToken)
    {

 
 
 
 
 
 
         await _repository.AddAsync(model, cancellationToken);
    }

    public async Task<bool> Update(IoTDevice model, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.DeviceId = model.DeviceId;
        existing.SerialNumber = model.SerialNumber;
        existing.LastSeen = model.LastSeen;
        existing.FirmwareVersion = model.FirmwareVersion;
        existing.Status = model.Status;
        existing.PowerSource = model.PowerSource;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public Task<IoTDevice?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<IoTDevice>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignDeviceModel(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDeviceModel(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignTenant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignTenant(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignSite(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignSite(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignRoom(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRoom(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignGateway(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignGateway(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignDigitalTwin(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignDigitalTwin(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignProvisioningRecord(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignProvisioningRecord(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToSensors(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromSensors(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToActuators(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromActuators(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToCertificates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCertificates(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToTelemetryStreams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromTelemetryStreams(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToCommandInvocations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCommandInvocations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToAlerts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAlerts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToDeviceGroups(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDeviceGroups(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToNetworkProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromNetworkProfiles(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
