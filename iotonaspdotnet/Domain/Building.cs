using iotonaspdotnet.Domain.Contracts;

namespace iotonaspdotnet.Domain;

public class Building
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long BuildingId { get; set; }
 public virtual string Name { get; set; }
public virtual Site Site { get; set; }
public virtual Floor Floors { get; set; }

    public static Building FromRequest(BuildingRequest request) {
        return new Building {
            Id = model.Id,
            Name = request.Name,
        };
}
