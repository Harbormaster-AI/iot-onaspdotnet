using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class IoTDevice
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual DeviceId  { get; set; }
 public virtual string  { get; set; }
 public virtual DateTime  { get; set; }
 public virtual FirmwareVersion  { get; set; }
public virtual DeviceModel  { get; set; }
public virtual Tenant  { get; set; }
public virtual Site  { get; set; }
public virtual Room  { get; set; }
public virtual Gateway  { get; set; }
public virtual SensorInstance  { get; set; }
public virtual ActuatorInstance  { get; set; }
public virtual DeviceCertificate  { get; set; }
public virtual DigitalTwin  { get; set; }
public virtual TelemetryStream  { get; set; }
public virtual CommandInvocation  { get; set; }
public virtual Alert  { get; set; }
public virtual ProvisioningRecord  { get; set; }
public virtual DeviceGroup  { get; set; }
public virtual NetworkProfile  { get; set; }
 public virtual DeviceStatus  { get; set; }
 public virtual PowerSource  { get; set; }
}
