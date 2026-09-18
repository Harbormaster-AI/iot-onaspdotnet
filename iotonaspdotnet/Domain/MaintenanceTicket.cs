using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Domain;

public class MaintenanceTicket
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long MaintenanceticketId { get; set; }
 public virtual string TicketNumber { get; set; }
 public virtual DateTime OpenedAt { get; set; }
 public virtual DateTime ClosedAt { get; set; }
public virtual IoTDevice Device { get; set; }
public virtual Tenant Tenant { get; set; }
 public virtual MaintenancePriority Priority { get; set; }
 public virtual MaintenanceStatus Status { get; set; }

    public static MaintenanceTicket FromRequest(MaintenanceTicketRequest request) {
        return new MaintenanceTicket {
            Id = request.Id,
            TicketNumber = request.TicketNumber,
            OpenedAt = request.OpenedAt,
            ClosedAt = request.ClosedAt,
            Priority = request.Priority,
            Status = request.Status,
        };
    }
}
