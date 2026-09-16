using iotonaspdotnet.Domain.Tenants;
using iotonaspdotnet.Domain.TelemetryStreams;
using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;
namespace iotonaspdotnet.Domain;

public class DataRetentionPolicy
{
    public Guid Id { get; set; } = Guid.NewGuid();

public virtual long dataretentionpolicyId { get; set; }
 public virtual string name { get; set; }
 public virtual int retentionDays { get; set; }
public virtual Tenant Tenant { get; set; }
public virtual TelemetryStream Streams { get; set; }
}
