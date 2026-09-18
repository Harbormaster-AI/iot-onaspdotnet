using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class Room
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? RoomId { get; set; } 
 public virtual string? Name { get; set; } 
public virtual Floor Floor { get; set; } 
public virtual ICollection<IoTDevice> Devices { get; set; } = new List<IoTDevice>();
public virtual ICollection<Gateway> Gateways { get; set; } = new List<Gateway>();

    public static Room FromRequest(RoomRequest request) {
        return new Room {
            Id = request.Id,
            Name = request.Name,
        };
    }
}
