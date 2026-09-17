using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class NetworkProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual string  { get; set; }
 public virtual string  { get; set; }
 public virtual string  { get; set; }
public virtual IoTDevice  { get; set; }
public virtual Gateway  { get; set; }
public virtual SimCard  { get; set; }
 public virtual ConnectivityType  { get; set; }
}
