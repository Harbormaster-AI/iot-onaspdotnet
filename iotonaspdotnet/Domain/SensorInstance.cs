using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class SensorInstance
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual NetworkProfile sensorinstanceId { get; set; }
								 public virtual NetworkProfile name { get; set; }
								 public virtual NetworkProfile unit { get; set; }
								 public virtual NetworkProfile samplingIntervalMs { get; set; }
								public virtual IoTDevice Device { get; set; }
								public virtual TelemetryStream TelemetryStreams { get; set; }
								 public virtual TelemetryStream SensorType { get; set; }
			}
