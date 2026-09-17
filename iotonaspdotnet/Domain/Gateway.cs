using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class Gateway
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual string  { get; set; }
public virtual Site  { get; set; }
public virtual Room  { get; set; }
public virtual IoTDevice  { get; set; }
public virtual EdgeApplication  { get; set; }
public virtual DeviceCertificate  { get; set; }
public virtual DigitalTwin  { get; set; }
public virtual NetworkProfile  { get; set; }
 public virtual DeviceStatus  { get; set; }
}
