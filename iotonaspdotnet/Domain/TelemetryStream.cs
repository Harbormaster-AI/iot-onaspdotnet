using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class TelemetryStream
{
    public Guid Id { get; set; } = Guid.NewGuid();

													public virtual long telemetrystreamId { get; set; }
											 public virtual string streamName { get; set; }
											 public virtual int retentionDays { get; set; }
											public virtual IoTDevice Device { get; set; }
											public virtual SensorInstance Sensor { get; set; }
											public virtual TelemetrySchema Schema { get; set; }
											public virtual MessagingEndpoint MessagingEndpoint { get; set; }
											public virtual DataRetentionPolicy RetentionPolicy { get; set; }
											 public virtual MessageQoS Qos { get; set; }
			}
