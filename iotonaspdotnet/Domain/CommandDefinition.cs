using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class CommandDefinition
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual DataRetentionPolicy commanddefinitionId { get; set; }
								 public virtual DataRetentionPolicy name { get; set; }
								 public virtual DataRetentionPolicy requestSchemaUri { get; set; }
								 public virtual DataRetentionPolicy responseSchemaUri { get; set; }
								 public virtual DataRetentionPolicy timeoutSeconds { get; set; }
								public virtual DeviceModel DeviceModel { get; set; }
								public virtual ActuatorInstance Actuators { get; set; }
								public virtual CommandInvocation CommandInvocations { get; set; }
			}
