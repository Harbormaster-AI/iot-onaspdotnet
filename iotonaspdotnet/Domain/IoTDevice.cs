using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class IoTDevice
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? IotdeviceId { get; set; } 
 public virtual DeviceId? DeviceId { get; set; } 
 public virtual string? SerialNumber { get; set; } 
 public virtual DateTime? LastSeen { get; set; } 
 public virtual FirmwareVersion? FirmwareVersion { get; set; } 
public virtual DeviceModel? DeviceModel { get; set; } 
public virtual Tenant? Tenant { get; set; } 
public virtual Site? Site { get; set; } 
public virtual Room? Room { get; set; } 
public virtual Gateway? Gateway { get; set; } 
public virtual SensorInstance? Sensors { get; set; } 
public virtual ActuatorInstance? Actuators { get; set; } 
public virtual DeviceCertificate? Certificates { get; set; } 
public virtual DigitalTwin? DigitalTwin { get; set; } 
public virtual TelemetryStream? TelemetryStreams { get; set; } 
public virtual CommandInvocation? CommandInvocations { get; set; } 
public virtual Alert? Alerts { get; set; } 
public virtual ProvisioningRecord? ProvisioningRecord { get; set; } 
public virtual DeviceGroup? DeviceGroups { get; set; } 
public virtual NetworkProfile? NetworkProfiles { get; set; } 
 public virtual DeviceStatus? Status { get; set; } 
 public virtual PowerSource? PowerSource { get; set; } 

    public static IoTDevice FromRequest(IoTDeviceRequest request) {
        return new IoTDevice {
            Id = request.Id,
            DeviceId = request.DeviceId,
            SerialNumber = request.SerialNumber,
            LastSeen = request.LastSeen,
            FirmwareVersion = request.FirmwareVersion,
            Status = request.Status,
            PowerSource = request.PowerSource,
        };
    }
}
