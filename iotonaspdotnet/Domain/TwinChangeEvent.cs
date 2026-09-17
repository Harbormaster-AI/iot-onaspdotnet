using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class TwinChangeEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual DeviceModel twinchangeeventId { get; set; }
								 public virtual DeviceModel eventId { get; set; }
								 public virtual DeviceModel occurredAt { get; set; }
								public virtual DigitalTwin Twin { get; set; }
								 public virtual DigitalTwin ChangeType { get; set; }
			}
