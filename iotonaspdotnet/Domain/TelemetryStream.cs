using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class TelemetryStream
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual string  { get; set; }
 public virtual int  { get; set; }
public virtual IoTDevice  { get; set; }
public virtual SensorInstance  { get; set; }
public virtual TelemetrySchema  { get; set; }
public virtual MessagingEndpoint  { get; set; }
public virtual DataRetentionPolicy  { get; set; }
 public virtual MessageQoS  { get; set; }
}
