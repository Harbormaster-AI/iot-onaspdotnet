namespace iotonaspdotnet.Domain;

public class TwinChangeEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long TwinchangeeventId { get; set; }
 public virtual string EventId { get; set; }
 public virtual DateTime OccurredAt { get; set; }
public virtual DigitalTwin Twin { get; set; }
 public virtual TwinChangeType ChangeType { get; set; }
}
