using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class CommandInvocation
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual long commandinvocationId { get; set; }
								 public virtual string invocationId { get; set; }
								 public virtual DateTime requestedAt { get; set; }
								 public virtual DateTime completedAt { get; set; }
								public virtual IoTDevice Device { get; set; }
								public virtual CommandDefinition CommandDefinition { get; set; }
								public virtual ActuatorInstance Actuator { get; set; }
								public virtual TenantUser User { get; set; }
								 public virtual CommandStatus Status { get; set; }
			}
