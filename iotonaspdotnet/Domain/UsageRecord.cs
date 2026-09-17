using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class UsageRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual IoTDevice usagerecordId { get; set; }
								 public virtual IoTDevice periodStart { get; set; }
								 public virtual IoTDevice periodEnd { get; set; }
								 public virtual IoTDevice messagesSent { get; set; }
								 public virtual IoTDevice dataVolumeMB { get; set; }
								public virtual Tenant Tenant { get; set; }
								public virtual IoTDevice Device { get; set; }
								public virtual ConnectivityPlan ConnectivityPlan { get; set; }
			}
