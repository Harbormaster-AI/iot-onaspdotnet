using iotonaspdotnet.Domain.IoTDevices;
using iotonaspdotnet.Domain.Gateways;
using iotonaspdotnet.Domain.TwinTemplates;
using iotonaspdotnet.Domain.TwinChangeEvents;
using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;
namespace iotonaspdotnet.Domain;

public class DigitalTwin
{
    public Guid Id { get; set; } = Guid.NewGuid();

public virtual long digitaltwinId { get; set; }
 public virtual string twinId { get; set; }
 public virtual int desiredStateVersion { get; set; }
 public virtual int reportedStateVersion { get; set; }
 public virtual DateTime lastSyncAt { get; set; }
public virtual IoTDevice Device { get; set; }
public virtual Gateway Gateway { get; set; }
public virtual TwinTemplate Template { get; set; }
public virtual TwinChangeEvent ChangeEvents { get; set; }
}
