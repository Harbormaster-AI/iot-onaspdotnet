using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class TelemetryStream
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long TelemetrystreamId { get; set; }
 public virtual string StreamName { get; set; }
 public virtual int RetentionDays { get; set; }
public virtual IoTDevice Device { get; set; }
public virtual SensorInstance Sensor { get; set; }
public virtual TelemetrySchema Schema { get; set; }
public virtual MessagingEndpoint MessagingEndpoint { get; set; }
public virtual DataRetentionPolicy RetentionPolicy { get; set; }
 public virtual MessageQoS Qos { get; set; }

    public static TelemetryStream FromRequest(TelemetryStreamRequest request) {
        return new TelemetryStream {
            Id = request.Id,
            StreamName = request.StreamName,
            RetentionDays = request.RetentionDays,
            Qos = request.Qos,
        };
    }
}
