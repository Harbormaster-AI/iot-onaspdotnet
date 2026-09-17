namespace iotonaspdotnet.Domain;

public class SoftwareUpdateExecution
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long SoftwareupdateexecutionId { get; set; }
 public virtual DateTime StartedAt { get; set; }
 public virtual DateTime CompletedAt { get; set; }
public virtual SoftwareUpdateCampaign Campaign { get; set; }
public virtual IoTDevice Device { get; set; }
 public virtual UpdateStatus Status { get; set; }
}
