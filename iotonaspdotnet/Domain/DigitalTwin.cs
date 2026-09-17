namespace iotonaspdotnet.Domain;

public class DigitalTwin
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long DigitaltwinId { get; set; }
 public virtual string TwinId { get; set; }
 public virtual int DesiredStateVersion { get; set; }
 public virtual int ReportedStateVersion { get; set; }
 public virtual DateTime LastSyncAt { get; set; }
public virtual IoTDevice Device { get; set; }
public virtual Gateway Gateway { get; set; }
public virtual TwinTemplate Template { get; set; }
public virtual TwinChangeEvent ChangeEvents { get; set; }
}
