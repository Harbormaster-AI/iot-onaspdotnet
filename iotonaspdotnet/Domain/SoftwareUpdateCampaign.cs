using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class SoftwareUpdateCampaign
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual string  { get; set; }
 public virtual DateTime  { get; set; }
 public virtual DateTime  { get; set; }
public virtual FirmwareRelease  { get; set; }
public virtual DeviceGroup  { get; set; }
public virtual SoftwareUpdateExecution  { get; set; }
 public virtual UpdateCampaignStatus  { get; set; }
}
