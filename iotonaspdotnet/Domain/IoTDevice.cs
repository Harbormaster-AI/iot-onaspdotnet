using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class IoTDevice
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? IotdeviceId { get; set; } 
 public virtual DeviceId? DeviceId { get; set; } 
 public virtual string? SerialNumber { get; set; } 
 public virtual DateTime_? LastSeen { get; set; } 
 public virtual FirmwareVersion? FirmwareVersion { get; set; } 
public virtual DeviceModel? DeviceModel { get; set; } 
public virtual Tenant? Tenant { get; set; } 
public virtual Site? Site { get; set; } 
public virtual Room? Room { get; set; } 
public virtual Gateway? Gateway { get; set; } 
public virtual ICollection<SensorInstance> Sensors { get; set; } = new List<SensorInstance>();
public virtual ICollection<ActuatorInstance> Actuators { get; set; } = new List<ActuatorInstance>();
public virtual ICollection<DeviceCertificate> Certificates { get; set; } = new List<DeviceCertificate>();
public virtual DigitalTwin? DigitalTwin { get; set; } 
public virtual ICollection<TelemetryStream> TelemetryStreams { get; set; } = new List<TelemetryStream>();
public virtual ICollection<CommandInvocation> CommandInvocations { get; set; } = new List<CommandInvocation>();
public virtual ICollection<Alert> Alerts { get; set; } = new List<Alert>();
public virtual ProvisioningRecord? ProvisioningRecord { get; set; } 
public virtual ICollection<DeviceGroup> DeviceGroups { get; set; } = new List<DeviceGroup>();
public virtual ICollection<NetworkProfile> NetworkProfiles { get; set; } = new List<NetworkProfile>();
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
