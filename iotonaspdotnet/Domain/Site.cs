using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class Site
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual string  { get; set; }
 public virtual Address  { get; set; }
 public virtual string  { get; set; }
 public virtual decimal  { get; set; }
 public virtual decimal  { get; set; }
public virtual Tenant  { get; set; }
public virtual Building  { get; set; }
public virtual IoTDevice  { get; set; }
public virtual Gateway  { get; set; }
}
