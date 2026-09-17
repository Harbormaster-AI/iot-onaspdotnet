using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class ConnectivityPlan
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual ConnectivityPlan connectivityplanId { get; set; }
								 public virtual ConnectivityPlan name { get; set; }
								 public virtual ConnectivityPlan dataCapMB { get; set; }
								 public virtual ConnectivityPlan billingCycleDays { get; set; }
								public virtual SimCard SimCards { get; set; }
								public virtual Tenant Tenant { get; set; }
			}
