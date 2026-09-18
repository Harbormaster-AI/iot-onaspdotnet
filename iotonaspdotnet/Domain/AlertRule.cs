using iotonaspdotnet.Domain.Contracts;

namespace iotonaspdotnet.Domain;

public class AlertRule
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long AlertruleId { get; set; }
 public virtual string Name { get; set; }
 public virtual string Expression { get; set; }
public virtual Tenant Tenant { get; set; }
public virtual TelemetryStream Streams { get; set; }
public virtual Alert Alerts { get; set; }
 public virtual AlertSeverity Severity { get; set; }

    public static AlertRule FromRequest(AlertRuleRequest request) {
        return new AlertRule {
            Id = request.Id,
            Name = request.Name,
            Expression = request.Expression,
            Severity = request.Severity,
        };
    }
}
