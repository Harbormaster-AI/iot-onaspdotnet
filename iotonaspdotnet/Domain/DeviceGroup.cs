
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class DeviceGroup
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DevicegroupId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Criteria { get; set; } 
public virtual Tenant? Tenant { get; set; } 
public virtual ICollection<IoTDevice> Devices { get; set; } = new List<IoTDevice>();

    public static DeviceGroup FromRequest(DeviceGroupRequest request) {
        return new DeviceGroup {
            Id = request.Id,
            Name = request.Name,
            Criteria = request.Criteria,
        };
    }
}
