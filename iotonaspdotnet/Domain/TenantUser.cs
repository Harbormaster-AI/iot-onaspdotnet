using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class TenantUser
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual long tenantuserId { get; set; }
								 public virtual string firstName { get; set; }
								 public virtual string lastName { get; set; }
								 public virtual string email { get; set; }
								public virtual Tenant Tenant { get; set; }
								public virtual CommandInvocation CommandInvocations { get; set; }
								 public virtual UserRole Role { get; set; }
			}
