using iotonaspdotnet.Domain.SimCards;
using iotonaspdotnet.Domain.Tenants;
using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;
namespace iotonaspdotnet.Domain;

public class ConnectivityPlan
{
    public Guid Id { get; set; } = Guid.NewGuid();

public virtual long connectivityplanId { get; set; }
 public virtual string name { get; set; }
 public virtual int dataCapMB { get; set; }
 public virtual int billingCycleDays { get; set; }
public virtual SimCard SimCards { get; set; }
public virtual Tenant Tenant { get; set; }
}
