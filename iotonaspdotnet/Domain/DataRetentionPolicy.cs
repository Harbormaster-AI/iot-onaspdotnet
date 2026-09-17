using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class DataRetentionPolicy
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual string  { get; set; }
 public virtual int  { get; set; }
public virtual Tenant  { get; set; }
public virtual TelemetryStream  { get; set; }
}
