using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class ActuatorInstance
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ActuatorinstanceId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual TopicName? CommandTopic { get; set; } 
public virtual IoTDevice? Device { get; set; } 
public virtual ICollection<CommandDefinition> SupportedCommands { get; set; } = new List<CommandDefinition>();
 public virtual ActuatorType? ActuatorType { get; set; } 

    public static ActuatorInstance FromRequest(ActuatorInstanceRequest request) {
        return new ActuatorInstance {
            Id = request.Id,
            Name = request.Name,
            CommandTopic = request.CommandTopic,
            ActuatorType = request.ActuatorType,
        };
    }
}
