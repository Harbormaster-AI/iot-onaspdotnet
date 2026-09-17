using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class CommandDefinition
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual string  { get; set; }
 public virtual Uri_  { get; set; }
 public virtual Uri_  { get; set; }
 public virtual int  { get; set; }
public virtual DeviceModel  { get; set; }
public virtual ActuatorInstance  { get; set; }
public virtual CommandInvocation  { get; set; }
}
