using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class ActuatorInstance
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual string  { get; set; }
 public virtual TopicName  { get; set; }
public virtual IoTDevice  { get; set; }
public virtual CommandDefinition  { get; set; }
 public virtual ActuatorType  { get; set; }
}
