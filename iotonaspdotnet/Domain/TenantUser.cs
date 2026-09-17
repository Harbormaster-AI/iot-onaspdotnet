using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class TenantUser
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual UsageRecord tenantuserId { get; set; }
								 public virtual UsageRecord firstName { get; set; }
								 public virtual UsageRecord lastName { get; set; }
								 public virtual UsageRecord email { get; set; }
								public virtual Tenant Tenant { get; set; }
								public virtual CommandInvocation CommandInvocations { get; set; }
								 public virtual CommandInvocation Role { get; set; }
			}
