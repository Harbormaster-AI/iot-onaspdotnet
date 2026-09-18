using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class CommandDefinition
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CommanddefinitionId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual Uri_? RequestSchemaUri { get; set; } 
 public virtual Uri_? ResponseSchemaUri { get; set; } 
 public virtual int? TimeoutSeconds { get; set; } 
public virtual DeviceModel DeviceModel { get; set; } 
public virtual ICollection<ActuatorInstance> Actuators { get; set; } = new List<ActuatorInstance>();
public virtual ICollection<CommandInvocation> CommandInvocations { get; set; } = new List<CommandInvocation>();

    public static CommandDefinition FromRequest(CommandDefinitionRequest request) {
        return new CommandDefinition {
            Id = request.Id,
            Name = request.Name,
            RequestSchemaUri = request.RequestSchemaUri,
            ResponseSchemaUri = request.ResponseSchemaUri,
            TimeoutSeconds = request.TimeoutSeconds,
        };
    }
}
