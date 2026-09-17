using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class TenantUser
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual string  { get; set; }
 public virtual string  { get; set; }
 public virtual string  { get; set; }
public virtual Tenant  { get; set; }
public virtual CommandInvocation  { get; set; }
 public virtual UserRole  { get; set; }
}
