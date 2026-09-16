using iotonaspdotnet.Domain.IoTDevices;
using iotonaspdotnet.Domain.Gateways;
using iotonaspdotnet.Domain.SimCards;
using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;
namespace iotonaspdotnet.Domain;

public class NetworkProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();

public virtual long networkprofileId { get; set; }
 public virtual string profileName { get; set; }
 public virtual string ssid { get; set; }
 public virtual string apn { get; set; }
public virtual IoTDevice Device { get; set; }
public virtual Gateway Gateway { get; set; }
public virtual SimCard SimCard { get; set; }
 public virtual ConnectivityType ConnectivityType { get; set; }
}
