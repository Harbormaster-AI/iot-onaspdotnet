using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class AccessPolicy
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long AccesspolicyId { get; set; }
 public virtual string Name { get; set; }
 public virtual string Scope { get; set; }
 public virtual DateTime ExpiresAt { get; set; }
public virtual Tenant Tenant { get; set; }
public virtual ApiKey ApiKeys { get; set; }
public virtual TenantUser Users { get; set; }
}
