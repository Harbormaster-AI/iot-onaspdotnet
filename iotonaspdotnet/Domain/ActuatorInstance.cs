namespace iotonaspdotnet.Domain;

public class ActuatorInstance
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long ActuatorinstanceId { get; set; }
 public virtual string Name { get; set; }
 public virtual TopicName CommandTopic { get; set; }
public virtual IoTDevice Device { get; set; }
public virtual CommandDefinition SupportedCommands { get; set; }
 public virtual ActuatorType ActuatorType { get; set; }
}
