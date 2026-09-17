using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class DataRetentionPolicy
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long DataretentionpolicyId { get; set; }
 public virtual string Name { get; set; }
 public virtual int RetentionDays { get; set; }
public virtual Tenant Tenant { get; set; }
public virtual TelemetryStream Streams { get; set; }
}
