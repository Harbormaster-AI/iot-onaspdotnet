
using iotonaspdotnet.Contracts;
using iotonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace iotonaspdotnet.Persistence;

public class IoTDeviceRepository : IIoTDeviceRepository
{
    private readonly ApplicationDbContext _db;

    public IoTDeviceRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IoTDevice?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.IoTDevices
            .Include(x => x.DeviceModel)
            .Include(x => x.Tenant)
            .Include(x => x.Site)
            .Include(x => x.Room)
            .Include(x => x.Gateway)
            .Include(x => x.DigitalTwin)
            .Include(x => x.ProvisioningRecord)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<IoTDevice>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.IoTDevices
            .AsNoTracking()
            .Include(x => x.DeviceModel)
            .Include(x => x.Tenant)
            .Include(x => x.Site)
            .Include(x => x.Room)
            .Include(x => x.Gateway)
            .Include(x => x.DigitalTwin)
            .Include(x => x.ProvisioningRecord)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(IoTDevice ioTDevice, CancellationToken cancellationToken)
    {
        _db.IoTDevices.Add(ioTDevice);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(IoTDevice ioTDevice, CancellationToken cancellationToken)
    {
        _db.IoTDevices.Update(ioTDevice);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(IoTDevice ioTDevice, CancellationToken cancellationToken)
    {
        _db.IoTDevices.Remove(ioTDevice);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToSensorsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.SensorInstances
            .Where(sensorInstance =>
                request.ChildIds.Contains(sensorInstance.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    sensorInstance =>
                        EF.Property<Guid?>(
                            sensorInstance,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromSensorsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.SensorInstances
            .Where(sensorInstance =>
                request.ChildIds.Contains(sensorInstance.Id) &&
                EF.Property<Guid?>(
                    sensorInstance,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    sensorInstance =>
                        EF.Property<Guid?>(
                            sensorInstance,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToActuatorsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ActuatorInstances
            .Where(actuatorInstance =>
                request.ChildIds.Contains(actuatorInstance.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    actuatorInstance =>
                        EF.Property<Guid?>(
                            actuatorInstance,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromActuatorsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ActuatorInstances
            .Where(actuatorInstance =>
                request.ChildIds.Contains(actuatorInstance.Id) &&
                EF.Property<Guid?>(
                    actuatorInstance,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    actuatorInstance =>
                        EF.Property<Guid?>(
                            actuatorInstance,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToCertificatesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DeviceCertificates
            .Where(deviceCertificate =>
                request.ChildIds.Contains(deviceCertificate.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    deviceCertificate =>
                        EF.Property<Guid?>(
                            deviceCertificate,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCertificatesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DeviceCertificates
            .Where(deviceCertificate =>
                request.ChildIds.Contains(deviceCertificate.Id) &&
                EF.Property<Guid?>(
                    deviceCertificate,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    deviceCertificate =>
                        EF.Property<Guid?>(
                            deviceCertificate,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToTelemetryStreamsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TelemetryStreams
            .Where(telemetryStream =>
                request.ChildIds.Contains(telemetryStream.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    telemetryStream =>
                        EF.Property<Guid?>(
                            telemetryStream,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromTelemetryStreamsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.TelemetryStreams
            .Where(telemetryStream =>
                request.ChildIds.Contains(telemetryStream.Id) &&
                EF.Property<Guid?>(
                    telemetryStream,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    telemetryStream =>
                        EF.Property<Guid?>(
                            telemetryStream,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToCommandInvocationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CommandInvocations
            .Where(commandInvocation =>
                request.ChildIds.Contains(commandInvocation.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    commandInvocation =>
                        EF.Property<Guid?>(
                            commandInvocation,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCommandInvocationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CommandInvocations
            .Where(commandInvocation =>
                request.ChildIds.Contains(commandInvocation.Id) &&
                EF.Property<Guid?>(
                    commandInvocation,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    commandInvocation =>
                        EF.Property<Guid?>(
                            commandInvocation,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToAlertsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Alerts
            .Where(alert =>
                request.ChildIds.Contains(alert.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    alert =>
                        EF.Property<Guid?>(
                            alert,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAlertsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Alerts
            .Where(alert =>
                request.ChildIds.Contains(alert.Id) &&
                EF.Property<Guid?>(
                    alert,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    alert =>
                        EF.Property<Guid?>(
                            alert,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToDeviceGroupsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DeviceGroups
            .Where(deviceGroup =>
                request.ChildIds.Contains(deviceGroup.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    deviceGroup =>
                        EF.Property<Guid?>(
                            deviceGroup,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDeviceGroupsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DeviceGroups
            .Where(deviceGroup =>
                request.ChildIds.Contains(deviceGroup.Id) &&
                EF.Property<Guid?>(
                    deviceGroup,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    deviceGroup =>
                        EF.Property<Guid?>(
                            deviceGroup,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }


    public async Task AddToNetworkProfilesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.NetworkProfiles
            .Where(networkProfile =>
                request.ChildIds.Contains(networkProfile.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    networkProfile =>
                        EF.Property<Guid?>(
                            networkProfile,
                            "UsageRecord_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromNetworkProfilesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.NetworkProfiles
            .Where(networkProfile =>
                request.ChildIds.Contains(networkProfile.Id) &&
                EF.Property<Guid?>(
                    networkProfile,
                    "UsageRecord_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    networkProfile =>
                        EF.Property<Guid?>(
                            networkProfile,
                            "UsageRecord_Id"),
                    (Guid?)null));
    }

}
