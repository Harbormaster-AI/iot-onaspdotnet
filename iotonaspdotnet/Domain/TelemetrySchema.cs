using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class TelemetrySchema
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? TelemetryschemaId { get; set; } 
 public virtual string? SchemaId { get; set; } 
 public virtual Uri_? SchemaUri { get; set; } 
public virtual ICollection<TelemetryStream>? Streams { get; set; } = new List<TelemetryStream>()
 public virtual TelemetryEncoding? Encoding { get; set; } 

    public static TelemetrySchema FromRequest(TelemetrySchemaRequest request) {
        return new TelemetrySchema {
            Id = request.Id,
            SchemaId = request.SchemaId,
            SchemaUri = request.SchemaUri,
            Encoding = request.Encoding,
        };
    }
}
