namespace iotonaspdotnet.Domain;

public class TelemetrySchema
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long TelemetryschemaId { get; set; }
 public virtual string SchemaId { get; set; }
 public virtual Uri_ SchemaUri { get; set; }
public virtual TelemetryStream Streams { get; set; }
 public virtual TelemetryEncoding Encoding { get; set; }
}
