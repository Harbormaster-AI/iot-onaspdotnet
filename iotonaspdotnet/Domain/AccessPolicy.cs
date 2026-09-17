using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class AccessPolicy
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual TelemetryStream accesspolicyId { get; set; }
								 public virtual TelemetryStream name { get; set; }
								 public virtual TelemetryStream scope { get; set; }
								 public virtual TelemetryStream expiresAt { get; set; }
								public virtual Tenant Tenant { get; set; }
								public virtual ApiKey ApiKeys { get; set; }
								public virtual TenantUser Users { get; set; }
			}
