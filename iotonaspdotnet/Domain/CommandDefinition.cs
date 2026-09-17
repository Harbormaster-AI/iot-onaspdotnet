using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class CommandDefinition
{
    public Guid Id { get; set; } = Guid.NewGuid();

													public virtual long commanddefinitionId { get; set; }
											 public virtual string name { get; set; }
											 public virtual Uri_ requestSchemaUri { get; set; }
											 public virtual Uri_ responseSchemaUri { get; set; }
											 public virtual int timeoutSeconds { get; set; }
											public virtual DeviceModel DeviceModel { get; set; }
											public virtual ActuatorInstance Actuators { get; set; }
											public virtual CommandInvocation CommandInvocations { get; set; }
			}
