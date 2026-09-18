using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IIoTDeviceService
{
    Task<IoTDevice?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<IoTDevice>> GetAll(CancellationToken cancellationToken);
    Task Create(IoTDeviceRequest request , CancellationToken cancellationToken);
    Task<bool> Update(IoTDeviceRequest request, CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);


}

public class IoTDeviceService : IIoTDeviceService
{
    private readonly IIoTDeviceRepository _repository;

    public IoTDeviceService(
        IIoTDeviceRepository repository )
    {
        _repository = repository;
    }

    public Task<IoTDevice?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(identifier.getId(), cancellationToken);

    public Task<IReadOnlyList<IoTDevice>> GetAll(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task Create(IoTDeviceRequest request, CancellationToken cancellationToken)
    {
        var deviceModel = await _deviceModels.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("DeviceModel not found.");

        if (deviceModel.DeviceModel is not null)
        {
            throw new InvalidOperationException("DeviceModel:DeviceModel already has a(n) IoTDevice (1:1 relationship).");
        }
        var tenant = await _tenants.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.Tenant is not null)
        {
            throw new InvalidOperationException("Tenant:Tenant already has a(n) IoTDevice (1:1 relationship).");
        }
        var site = await _sites.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Site not found.");

        if (site.Site is not null)
        {
            throw new InvalidOperationException("Site:Site already has a(n) IoTDevice (1:1 relationship).");
        }
        var room = await _rooms.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Room not found.");

        if (room.Room is not null)
        {
            throw new InvalidOperationException("Room:Room already has a(n) IoTDevice (1:1 relationship).");
        }
        var gateway = await _gateways.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Gateway not found.");

        if (gateway.Gateway is not null)
        {
            throw new InvalidOperationException("Gateway:Gateway already has a(n) IoTDevice (1:1 relationship).");
        }
        var digitalTwin = await _digitalTwins.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("DigitalTwin not found.");

        if (digitalTwin.DigitalTwin is not null)
        {
            throw new InvalidOperationException("DigitalTwin:DigitalTwin already has a(n) IoTDevice (1:1 relationship).");
        }
        var provisioningRecord = await _provisioningRecords.Get(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("ProvisioningRecord not found.");

        if (provisioningRecord.ProvisioningRecord is not null)
        {
            throw new InvalidOperationException("ProvisioningRecord:ProvisioningRecord already has a(n) IoTDevice (1:1 relationship).");
        }
        await _repository.AddAsync(request, cancellationToken);
    }

    public async Task<bool> Update(IoTDeviceRequest request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        existing.DeviceId = request.DeviceId;
        existing.SerialNumber = request.SerialNumber;
        existing.LastSeen = request.LastSeen;
        existing.FirmwareVersion = request.FirmwareVersion;
        existing.DeviceModel = request.DeviceModel;
        existing.Tenant = request.Tenant;
        existing.Site = request.Site;
        existing.Room = request.Room;
        existing.Gateway = request.Gateway;
        existing.Sensors = request.Sensors;
        existing.Actuators = request.Actuators;
        existing.Certificates = request.Certificates;
        existing.DigitalTwin = request.DigitalTwin;
        existing.TelemetryStreams = request.TelemetryStreams;
        existing.CommandInvocations = request.CommandInvocations;
        existing.Alerts = request.Alerts;
        existing.ProvisioningRecord = request.ProvisioningRecord;
        existing.DeviceGroups = request.DeviceGroups;
        existing.NetworkProfiles = request.NetworkProfiles;
        existing.Status = request.Status;
        existing.PowerSource = request.PowerSource;

        existing.DeviceId = ioTDevice.DeviceId;
        existing.SerialNumber = ioTDevice.SerialNumber;
        existing.LastSeen = ioTDevice.LastSeen;
        existing.FirmwareVersion = ioTDevice.FirmwareVersion;
        existing.Status = ioTDevice.Status;
        existing.PowerSource = ioTDevice.PowerSource;

        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }


}
