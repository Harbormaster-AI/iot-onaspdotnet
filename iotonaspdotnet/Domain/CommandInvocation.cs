namespace iotonaspdotnet.Domain;

public class CommandInvocation
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long CommandinvocationId { get; set; }
 public virtual string InvocationId { get; set; }
 public virtual DateTime RequestedAt { get; set; }
 public virtual DateTime CompletedAt { get; set; }
public virtual IoTDevice Device { get; set; }
public virtual CommandDefinition CommandDefinition { get; set; }
public virtual ActuatorInstance Actuator { get; set; }
public virtual TenantUser User { get; set; }
 public virtual CommandStatus Status { get; set; }
}
