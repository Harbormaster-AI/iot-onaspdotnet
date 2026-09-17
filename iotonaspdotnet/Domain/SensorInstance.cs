using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class SensorInstance
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual string  { get; set; }
 public virtual string  { get; set; }
 public virtual int  { get; set; }
public virtual IoTDevice  { get; set; }
public virtual TelemetryStream  { get; set; }
 public virtual SensorType  { get; set; }
}
