using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class Room
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual Room roomId { get; set; }
								 public virtual Room name { get; set; }
								public virtual Floor Floor { get; set; }
								public virtual IoTDevice Devices { get; set; }
								public virtual Gateway Gateways { get; set; }
			}
