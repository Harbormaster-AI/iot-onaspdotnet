using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class TwinChangeEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual string  { get; set; }
 public virtual DateTime  { get; set; }
public virtual DigitalTwin  { get; set; }
 public virtual TwinChangeType  { get; set; }
}
