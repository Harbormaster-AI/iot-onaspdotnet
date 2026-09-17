using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class SensorInstance
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual long sensorinstanceId { get; set; }
								 public virtual string name { get; set; }
								 public virtual string unit { get; set; }
								 public virtual int samplingIntervalMs { get; set; }
								