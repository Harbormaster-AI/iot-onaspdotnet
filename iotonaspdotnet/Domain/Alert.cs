using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;
namespace iotonaspdotnet.Domain;

public class Alert
{
    public Guid Id { get; set; } = Guid.NewGuid();

public virtual long alertId { get; set; }
 public virtual DateTime raisedAt { get; set; }
 public virtual DateTime clearedAt { get; set; }
 public virtual string message { get; set; }
public virtual IoTDevice Device { get; set; }
public virtual AlertRule AlertRule { get; set; }
 public virtual AlertStatus Status { get; set; }
}
