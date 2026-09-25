
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class Floor
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? FloorId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual int? Level { get; set; } 
public virtual Building? Building { get; set; } 
public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

    public static Floor FromRequest(FloorRequest request) {
        return new Floor {
            Id = request.Id,
            Name = request.Name,
            Level = request.Level,
        };
    }
}
