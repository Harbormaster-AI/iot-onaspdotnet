using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class SoftwareUpdateExecution
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual SoftwareUpdateExecution softwareupdateexecutionId { get; set; }
								 public virtual SoftwareUpdateExecution startedAt { get; set; }
								 public virtual SoftwareUpdateExecution completedAt { get; set; }
								public virtual SoftwareUpdateCampaign Campaign { get; set; }
								public virtual IoTDevice Device { get; set; }
								 public virtual IoTDevice Status { get; set; }
			}
