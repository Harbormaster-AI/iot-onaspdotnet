using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class SimCard
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual string  { get; set; }
 public virtual string  { get; set; }
 public virtual string  { get; set; }
public virtual NetworkProfile  { get; set; }
public virtual Tenant  { get; set; }
public virtual ConnectivityPlan  { get; set; }
 public virtual SimStatus  { get; set; }
}
