using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;
namespace iotonaspdotnet.Domain;

public class DeviceGroup
{
    public Guid Id { get; set; } = Guid.NewGuid();

public virtual long devicegroupId { get; set; }
 public virtual string name { get; set; }
 public virtual string criteria { get; set; }
public virtual Tenant Tenant { get; set; }
public virtual IoTDevice Devices { get; set; }
}
