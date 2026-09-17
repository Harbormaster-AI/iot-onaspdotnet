using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class Floor
{
    public Guid Id { get; set; } = Guid.NewGuid();

													public virtual long floorId { get; set; }
											 public virtual string name { get; set; }
											 public virtual int level { get; set; }
											public virtual Building Building { get; set; }
											public virtual Room Rooms { get; set; }
			}
