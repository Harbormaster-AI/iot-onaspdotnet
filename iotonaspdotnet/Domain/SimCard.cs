using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class SimCard
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual SimCard simcardId { get; set; }
								 public virtual SimCard iccid { get; set; }
								 public virtual SimCard imsi { get; set; }
								 public virtual SimCard carrier { get; set; }
								public virtual NetworkProfile NetworkProfiles { get; set; }
								public virtual Tenant Tenant { get; set; }
								public virtual ConnectivityPlan ConnectivityPlan { get; set; }
								 public virtual ConnectivityPlan Status { get; set; }
			}
