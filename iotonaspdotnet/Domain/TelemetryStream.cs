using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class TelemetryStream
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual TelemetryStream telemetrystreamId { get; set; }
								 public virtual TelemetryStream streamName { get; set; }
								 public virtual TelemetryStream retentionDays { get; set; }
								public virtual IoTDevice Device { get; set; }
								public virtual SensorInstance Sensor { get; set; }
								public virtual TelemetrySchema Schema { get; set; }
								public virtual MessagingEndpoint MessagingEndpoint { get; set; }
								public virtual DataRetentionPolicy RetentionPolicy { get; set; }
								 public virtual DataRetentionPolicy Qos { get; set; }
			}
