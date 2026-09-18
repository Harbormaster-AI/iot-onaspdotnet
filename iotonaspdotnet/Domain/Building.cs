using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class Building
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? BuildingId { get; set; } 
 public virtual string? Name { get; set; } 
public virtual Site Site { get; set; } 
public virtual ICollection<Floor> Floors { get; set; } = new List<Floor>();

    public static Building FromRequest(BuildingRequest request) {
        return new Building {
            Id = request.Id,
            Name = request.Name,
        };
    }
}
