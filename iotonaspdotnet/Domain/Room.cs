using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class Room
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long RoomId { get; set; }
 public virtual string Name { get; set; }
public virtual Floor Floor { get; set; }
public virtual IoTDevice Devices { get; set; }
public virtual Gateway Gateways { get; set; }
}
