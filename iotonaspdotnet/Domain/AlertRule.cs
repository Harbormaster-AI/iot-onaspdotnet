using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class AlertRule
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual TenantUser alertruleId { get; set; }
								 public virtual TenantUser name { get; set; }
								 public virtual TenantUser expression { get; set; }
								public virtual Tenant Tenant { get; set; }
								public virtual TelemetryStream Streams { get; set; }
								public virtual Alert Alerts { get; set; }
								 public virtual Alert Severity { get; set; }
			}
