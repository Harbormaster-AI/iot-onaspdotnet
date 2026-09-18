using iotonaspdotnet.Domain.Contracts;

namespace iotonaspdotnet.Domain;

public class TwinChangeEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long TwinchangeeventId { get; set; }
 public virtual string EventId { get; set; }
 public virtual DateTime OccurredAt { get; set; }
public virtual DigitalTwin Twin { get; set; }
 public virtual TwinChangeType ChangeType { get; set; }

    public static TwinChangeEvent FromRequest(TwinChangeEventRequest request) {
        return new TwinChangeEvent {
            Id = request.Id,
            EventId = request.EventId,
            OccurredAt = request.OccurredAt,
            ChangeType = request.ChangeType,
        };
    }
}
