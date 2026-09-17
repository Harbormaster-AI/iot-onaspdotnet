using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class AlertRule
{
    public Guid Id { get; set; } = Guid.NewGuid();

public virtual long alertruleId { get; set; }
 public virtual string name { get; set; }
 public virtual string expression { get; set; }
public virtual Tenant Tenant { get; set; }
public virtual TelemetryStream Streams { get; set; }
public virtual Alert Alerts { get; set; }
 public virtual AlertSeverity Severity { get; set; }
}
