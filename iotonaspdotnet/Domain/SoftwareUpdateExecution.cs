using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class SoftwareUpdateExecution
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual long softwareupdateexecutionId { get; set; }
								 public virtual DateTime startedAt { get; set; }
								 public virtual DateTime completedAt { get; set; }
								public virtual SoftwareUpdateCampaign Campaign { get; set; }
								public virtual IoTDevice Device { get; set; }
								 public virtual UpdateStatus Status { get; set; }
			}
