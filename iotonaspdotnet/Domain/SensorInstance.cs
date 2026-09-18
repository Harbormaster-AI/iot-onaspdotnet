using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class SensorInstance
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? SensorinstanceId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Unit { get; set; } 
 public virtual int? SamplingIntervalMs { get; set; } 
public virtual IoTDevice? Device { get; set; } 
public virtual TelemetryStream? TelemetryStreams { get; set; } 
 public virtual SensorType? SensorType { get; set; } 

    public static SensorInstance FromRequest(SensorInstanceRequest request) {
        return new SensorInstance {
            Id = request.Id,
            Name = request.Name,
            Unit = request.Unit,
            SamplingIntervalMs = request.SamplingIntervalMs,
            SensorType = request.SensorType,
        };
    }
}
