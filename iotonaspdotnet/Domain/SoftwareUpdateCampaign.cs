using iotonaspdotnet.Domain.Contracts;

namespace iotonaspdotnet.Domain;

public class SoftwareUpdateCampaign
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long SoftwareupdatecampaignId { get; set; }
 public virtual string CampaignCode { get; set; }
 public virtual DateTime ScheduledStart { get; set; }
 public virtual DateTime ScheduledEnd { get; set; }
public virtual FirmwareRelease FirmwareRelease { get; set; }
public virtual DeviceGroup DeviceGroup { get; set; }
public virtual SoftwareUpdateExecution Executions { get; set; }
 public virtual UpdateCampaignStatus Status { get; set; }

    public static SoftwareUpdateCampaign FromRequest(SoftwareUpdateCampaignRequest request) {
        return new SoftwareUpdateCampaign {
            Id = model.Id,
            CampaignCode = request.CampaignCode,
            ScheduledStart = request.ScheduledStart,
            ScheduledEnd = request.ScheduledEnd,
            Status = request.Status,
        };
}
