using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class MaintenanceTicket
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual long maintenanceticketId { get; set; }
								 public virtual string ticketNumber { get; set; }
								 public virtual DateTime openedAt { get; set; }
								 public virtual DateTime closedAt { get; set; }
								public virtual IoTDevice Device { get; set; }
								public virtual Tenant Tenant { get; set; }
								 public virtual MaintenancePriority Priority { get; set; }
								 public virtual MaintenanceStatus Status { get; set; }
			}
