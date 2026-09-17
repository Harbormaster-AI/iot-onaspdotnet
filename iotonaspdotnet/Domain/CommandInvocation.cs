using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class CommandInvocation
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual string  { get; set; }
 public virtual DateTime  { get; set; }
 public virtual DateTime  { get; set; }
public virtual IoTDevice  { get; set; }
public virtual CommandDefinition  { get; set; }
public virtual ActuatorInstance  { get; set; }
public virtual TenantUser  { get; set; }
 public virtual CommandStatus  { get; set; }
}
