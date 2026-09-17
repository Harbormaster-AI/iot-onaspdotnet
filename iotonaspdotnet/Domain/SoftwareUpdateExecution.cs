using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class SoftwareUpdateExecution
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long  { get; set; }
 public virtual DateTime  { get; set; }
 public virtual DateTime  { get; set; }
public virtual SoftwareUpdateCampaign  { get; set; }
public virtual IoTDevice  { get; set; }
 public virtual UpdateStatus  { get; set; }
}
