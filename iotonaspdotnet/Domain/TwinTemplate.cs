using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class TwinTemplate
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? TwintemplateId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual Uri_? SchemaUri { get; set; } 
 public virtual string? Version { get; set; } 
public virtual DeviceModel? DeviceModels { get; set; } 

    public static TwinTemplate FromRequest(TwinTemplateRequest request) {
        return new TwinTemplate {
            Id = request.Id,
            Name = request.Name,
            SchemaUri = request.SchemaUri,
            Version = request.Version,
        };
    }
}
