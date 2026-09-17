using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class AccessPolicy
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual string  { get; set; }
 public virtual string  { get; set; }
 public virtual DateTime  { get; set; }
public virtual Tenant  { get; set; }
public virtual ApiKey  { get; set; }
public virtual TenantUser  { get; set; }
}
