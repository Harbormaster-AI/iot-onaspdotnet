using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class TwinChangeEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();

													public virtual long twinchangeeventId { get; set; }
											 public virtual string eventId { get; set; }
											 public virtual DateTime occurredAt { get; set; }
											public virtual DigitalTwin Twin { get; set; }
											 public virtual TwinChangeType ChangeType { get; set; }
			}
