using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class SoftwareUpdateCampaign
{
    public Guid Id { get; set; } = Guid.NewGuid();

public virtual long softwareupdatecampaignId { get; set; }
 public virtual string campaignCode { get; set; }
 public virtual DateTime scheduledStart { get; set; }
 public virtual DateTime scheduledEnd { get; set; }
public virtual FirmwareRelease FirmwareRelease { get; set; }
public virtual DeviceGroup DeviceGroup { get; set; }
public virtual SoftwareUpdateExecution Executions { get; set; }
 public virtual UpdateCampaignStatus Status { get; set; }
}
