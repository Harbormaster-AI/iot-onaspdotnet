using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class MaintenanceTicket
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual DigitalTwin maintenanceticketId { get; set; }
								 public virtual DigitalTwin ticketNumber { get; set; }
								 public virtual DigitalTwin openedAt { get; set; }
								 public virtual DigitalTwin closedAt { get; set; }
								public virtual IoTDevice Device { get; set; }
								public virtual Tenant Tenant { get; set; }
								 public virtual Tenant Priority { get; set; }
								 public virtual Tenant Status { get; set; }
			}
