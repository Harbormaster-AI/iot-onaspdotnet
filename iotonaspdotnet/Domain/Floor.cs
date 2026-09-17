using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class Floor
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual Floor floorId { get; set; }
								 public virtual Floor name { get; set; }
								 public virtual Floor level { get; set; }
								public virtual Building Building { get; set; }
								public virtual Room Rooms { get; set; }
			}
