
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class DataRetentionPolicy
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DataretentionpolicyId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual int? RetentionDays { get; set; } 
public virtual Tenant? Tenant { get; set; } 
public virtual ICollection<TelemetryStream> Streams { get; set; } = new List<TelemetryStream>();

    public static DataRetentionPolicy FromRequest(DataRetentionPolicyRequest request) {
        return new DataRetentionPolicy {
            Id = request.Id,
            Name = request.Name,
            RetentionDays = request.RetentionDays,
        };
    }
}
