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
								