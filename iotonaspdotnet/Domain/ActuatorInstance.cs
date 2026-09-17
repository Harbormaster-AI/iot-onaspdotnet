using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class ActuatorInstance
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual TelemetryStream actuatorinstanceId { get; set; }
								 public virtual TelemetryStream name { get; set; }
								 public virtual TelemetryStream commandTopic { get; set; }
								public virtual IoTDevice Device { get; set; }
								public virtual CommandDefinition SupportedCommands { get; set; }
								 public virtual CommandDefinition ActuatorType { get; set; }
			}
