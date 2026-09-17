using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class ActuatorInstance
{
    public Guid Id { get; set; } = Guid.NewGuid();

public virtual long actuatorinstanceId { get; set; }
 public virtual string name { get; set; }
 public virtual TopicName commandTopic { get; set; }
public virtual IoTDevice Device { get; set; }
public virtual CommandDefinition SupportedCommands { get; set; }
 public virtual ActuatorType ActuatorType { get; set; }
}
