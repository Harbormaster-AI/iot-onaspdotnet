using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class Alert
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AlertId { get; set; }
 public virtual DateTime? RaisedAt { get; set; }
 public virtual DateTime? ClearedAt { get; set; }
 public virtual string? Message { get; set; }
public virtual IoTDevice? Device { get; set; }
public virtual AlertRule? AlertRule { get; set; }
 public virtual AlertStatus? Status { get; set; }

    public static Alert FromRequest(AlertRequest request) {
        return new Alert {
            Id = request.Id,
            RaisedAt = request.RaisedAt,
            ClearedAt = request.ClearedAt,
            Message = request.Message,
            Status = request.Status,
        };
    }
}
