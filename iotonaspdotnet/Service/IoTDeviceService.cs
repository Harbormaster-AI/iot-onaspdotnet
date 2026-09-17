using iotonaspdotnet.Domain;
using iotonaspdotnet.Persistence;

namespace iotonaspdotnet.Service;

public interface IIoTDeviceService
{
    Task<IoTDevice?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<IoTDevice>> GetAllAsync(CancellationToken cancellationToken);
    Task CreateAsync(IoTDevice ioTDevice, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(IoTDevice ioTDevice, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}

public class IoTDeviceService : IIoTDeviceService
{
    private readonly IIoTDeviceRepository _repository;
    private readonly IDeviceModelRepository _deviceModels;
    private readonly ITenantRepository _tenants;
    private readonly ISiteRepository _sites;
    private readonly IRoomRepository _rooms;
    private readonly IGatewayRepository _gateways;
    private readonly IDigitalTwinRepository _digitalTwins;
    private readonly IProvisioningRecordRepository _provisioningRecords;

    public IoTDeviceService(
        IDeviceModelRepository deviceModels,
        ITenantRepository tenants,
        ISiteRepository sites,
        IRoomRepository rooms,
        IGatewayRepository gateways,
        IDigitalTwinRepository digitalTwins,
        IProvisioningRecordRepository provisioningRecords,
        IIoTDeviceRepository repository )
    {
        _repository = repository;
        _deviceModels = deviceModels;
        _tenants = tenants;
        _sites = sites;
        _rooms = rooms;
        _gateways = gateways;
        _digitalTwins = digitalTwins;
        _provisioningRecords = provisioningRecords;
    }

    public Task<IoTDevice?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IReadOnlyList<IoTDevice>> GetAllAsync(CancellationToken cancellationToken)
        => _repository.GetAllAsync(cancellationToken);

    public async Task CreateAsync(IoTDevice ioTDevice, CancellationToken cancellationToken)
    {
        var deviceModel.DeviceModel = await _deviceModels.GetByIdAsync(ioTDevice.Id, cancellationToken)
            ?? throw new InvalidOperationException("DeviceModel not found.");

        if (deviceModel.DeviceModel is not null)
        {
            throw new InvalidOperationException("DeviceModel already has a(n) ioTDevice (1:1 relationship).");
        }
        var tenant.Tenant = await _tenants.GetByIdAsync(ioTDevice.Id, cancellationToken)
            ?? throw new InvalidOperationException("Tenant not found.");

        if (tenant.Tenant is not null)
        {
            throw new InvalidOperationException("Tenant already has a(n) ioTDevice (1:1 relationship).");
        }
        var site.Site = await _sites.GetByIdAsync(ioTDevice.Id, cancellationToken)
            ?? throw new InvalidOperationException("Site not found.");

        if (site.Site is not null)
        {
            throw new InvalidOperationException("Site already has a(n) ioTDevice (1:1 relationship).");
        }
        var room.Room = await _rooms.GetByIdAsync(ioTDevice.Id, cancellationToken)
            ?? throw new InvalidOperationException("Room not found.");

        if (room.Room is not null)
        {
            throw new InvalidOperationException("Room already has a(n) ioTDevice (1:1 relationship).");
        }
        var gateway.Gateway = await _gateways.GetByIdAsync(ioTDevice.Id, cancellationToken)
            ?? throw new InvalidOperationException("Gateway not found.");

        if (gateway.Gateway is not null)
        {
            throw new InvalidOperationException("Gateway already has a(n) ioTDevice (1:1 relationship).");
        }
        var digitalTwin.DigitalTwin = await _digitalTwins.GetByIdAsync(ioTDevice.Id, cancellationToken)
            ?? throw new InvalidOperationException("DigitalTwin not found.");

        if (digitalTwin.DigitalTwin is not null)
        {
            throw new InvalidOperationException("DigitalTwin already has a(n) ioTDevice (1:1 relationship).");
        }
        var provisioningRecord.ProvisioningRecord = await _provisioningRecords.GetByIdAsync(ioTDevice.Id, cancellationToken)
            ?? throw new InvalidOperationException("ProvisioningRecord not found.");

        if (provisioningRecord.ProvisioningRecord is not null)
        {
            throw new InvalidOperationException("ProvisioningRecord already has a(n) ioTDevice (1:1 relationship).");
        }
        await _repository.AddAsync(ioTDevice, cancellationToken);
    }

    public async Task<bool> UpdateAsync(IoTDevice ioTDevice, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(ioTDevice.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        // Keep 1:1 â do not reassign to a provisioningRecord who already has another ioTDevice.
        if (existing.Id != ioTDevice.Id)
        {
            var DeviceModel = await _deviceModels.GetByIdAsync(ioTDevice.Id, cancellationToken)
                ?? throw new InvalidOperationException("DeviceModel not found.");

            if (DeviceModel.IoTDevice is not null && DeviceModel.IoTDevice.Id != existing.Id)
            {
                throw new InvalidOperationException("Target deviceModel already has an ioTDevice (1:1 relationship).");
            }
            var Tenant = await _tenants.GetByIdAsync(ioTDevice.Id, cancellationToken)
                ?? throw new InvalidOperationException("Tenant not found.");

            if (Tenant.IoTDevice is not null && Tenant.IoTDevice.Id != existing.Id)
            {
                throw new InvalidOperationException("Target tenant already has an ioTDevice (1:1 relationship).");
            }
            var Site = await _sites.GetByIdAsync(ioTDevice.Id, cancellationToken)
                ?? throw new InvalidOperationException("Site not found.");

            if (Site.IoTDevice is not null && Site.IoTDevice.Id != existing.Id)
            {
                throw new InvalidOperationException("Target site already has an ioTDevice (1:1 relationship).");
            }
            var Room = await _rooms.GetByIdAsync(ioTDevice.Id, cancellationToken)
                ?? throw new InvalidOperationException("Room not found.");

            if (Room.IoTDevice is not null && Room.IoTDevice.Id != existing.Id)
            {
                throw new InvalidOperationException("Target room already has an ioTDevice (1:1 relationship).");
            }
            var Gateway = await _gateways.GetByIdAsync(ioTDevice.Id, cancellationToken)
                ?? throw new InvalidOperationException("Gateway not found.");

            if (Gateway.IoTDevice is not null && Gateway.IoTDevice.Id != existing.Id)
            {
                throw new InvalidOperationException("Target gateway already has an ioTDevice (1:1 relationship).");
            }
            var DigitalTwin = await _digitalTwins.GetByIdAsync(ioTDevice.Id, cancellationToken)
                ?? throw new InvalidOperationException("DigitalTwin not found.");

            if (DigitalTwin.IoTDevice is not null && DigitalTwin.IoTDevice.Id != existing.Id)
            {
                throw new InvalidOperationException("Target digitalTwin already has an ioTDevice (1:1 relationship).");
            }
            var ProvisioningRecord = await _provisioningRecords.GetByIdAsync(ioTDevice.Id, cancellationToken)
                ?? throw new InvalidOperationException("ProvisioningRecord not found.");

            if (ProvisioningRecord.IoTDevice is not null && ProvisioningRecord.IoTDevice.Id != existing.Id)
            {
                throw new InvalidOperationException("Target provisioningRecord already has an ioTDevice (1:1 relationship).");
            }
        }

        existing.DeviceId = ioTDevice.DeviceId;
        existing.SerialNumber = ioTDevice.SerialNumber;
        existing.LastSeen = ioTDevice.LastSeen;
        existing.FirmwareVersion = ioTDevice.FirmwareVersion;
        existing.Status = ioTDevice.Status;
        existing.PowerSource = ioTDevice.PowerSource;

        existing.Id = ioTDevice.Id;
        existing.Id = ioTDevice.Id;
        existing.Id = ioTDevice.Id;
        existing.Id = ioTDevice.Id;
        existing.Id = ioTDevice.Id;
        existing.Id = ioTDevice.Id;
        existing.Id = ioTDevice.Id;
        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }
}
