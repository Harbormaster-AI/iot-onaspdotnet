using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class DeviceModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual string  { get; set; }
 public virtual string  { get; set; }
 public virtual string  { get; set; }
public virtual DeviceVendor  { get; set; }
public virtual HardwareModule  { get; set; }
public virtual TwinTemplate  { get; set; }
public virtual FirmwareRelease  { get; set; }
public virtual CommandDefinition  { get; set; }
 public virtual ConnectivityType  { get; set; }
 public virtual TelemetryEncoding  { get; set; }
}
