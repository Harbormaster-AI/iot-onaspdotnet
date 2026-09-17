using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;
namespace iotonaspdotnet.Domain;

public class TelemetrySchema
{
    public Guid Id { get; set; } = Guid.NewGuid();

public virtual long telemetryschemaId { get; set; }
 public virtual string schemaId { get; set; }
 public virtual Uri schemaUri { get; set; }
public virtual TelemetryStream Streams { get; set; }
 public virtual TelemetryEncoding Encoding { get; set; }
}
