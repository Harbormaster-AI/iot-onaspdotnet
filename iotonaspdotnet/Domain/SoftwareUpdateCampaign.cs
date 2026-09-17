using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class SoftwareUpdateCampaign
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual TelemetryStream softwareupdatecampaignId { get; set; }
								 public virtual TelemetryStream campaignCode { get; set; }
								 public virtual TelemetryStream scheduledStart { get; set; }
								 public virtual TelemetryStream scheduledEnd { get; set; }
								public virtual FirmwareRelease FirmwareRelease { get; set; }
								public virtual DeviceGroup DeviceGroup { get; set; }
								public virtual SoftwareUpdateExecution Executions { get; set; }
								 public virtual SoftwareUpdateExecution Status { get; set; }
			}
