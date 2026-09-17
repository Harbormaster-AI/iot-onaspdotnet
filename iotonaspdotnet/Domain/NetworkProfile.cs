using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class NetworkProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long NetworkprofileId { get; set; }
 public virtual string ProfileName { get; set; }
 public virtual string Ssid { get; set; }
 public virtual string Apn { get; set; }
public virtual IoTDevice Device { get; set; }
public virtual Gateway Gateway { get; set; }
public virtual SimCard SimCard { get; set; }
 public virtual ConnectivityType ConnectivityType { get; set; }
}
