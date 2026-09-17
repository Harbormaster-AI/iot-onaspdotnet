using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class DigitalTwin
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual Tenant digitaltwinId { get; set; }
								 public virtual Tenant twinId { get; set; }
								 public virtual Tenant desiredStateVersion { get; set; }
								 public virtual Tenant reportedStateVersion { get; set; }
								 public virtual Tenant lastSyncAt { get; set; }
								public virtual IoTDevice Device { get; set; }
								public virtual Gateway Gateway { get; set; }
								public virtual TwinTemplate Template { get; set; }
								public virtual TwinChangeEvent ChangeEvents { get; set; }
			}
