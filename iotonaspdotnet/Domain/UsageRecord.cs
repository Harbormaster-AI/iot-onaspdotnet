using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class UsageRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual DateOnly  { get; set; }
 public virtual DateOnly  { get; set; }
 public virtual int  { get; set; }
 public virtual int  { get; set; }
public virtual Tenant  { get; set; }
public virtual IoTDevice  { get; set; }
public virtual ConnectivityPlan  { get; set; }
}
