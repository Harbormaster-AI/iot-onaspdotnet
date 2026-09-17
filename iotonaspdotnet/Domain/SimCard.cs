using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;
namespace iotonaspdotnet.Domain;

public class SimCard
{
    public Guid Id { get; set; } = Guid.NewGuid();

public virtual long simcardId { get; set; }
 public virtual string iccid { get; set; }
 public virtual string imsi { get; set; }
 public virtual string carrier { get; set; }
public virtual NetworkProfile NetworkProfiles { get; set; }
public virtual Tenant Tenant { get; set; }
public virtual ConnectivityPlan ConnectivityPlan { get; set; }
 public virtual SimStatus Status { get; set; }
}
