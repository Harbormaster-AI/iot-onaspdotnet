using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class ConnectivityPlan
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long ConnectivityplanId { get; set; }
 public virtual string Name { get; set; }
 public virtual int DataCapMB { get; set; }
 public virtual int BillingCycleDays { get; set; }
public virtual SimCard SimCards { get; set; }
public virtual Tenant Tenant { get; set; }
}
