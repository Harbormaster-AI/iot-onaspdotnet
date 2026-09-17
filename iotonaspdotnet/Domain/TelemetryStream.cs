using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class TelemetryStream
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual long telemetrystreamId { get; set; }
								 public virtual string streamName { get; set; }
								 public virtual int retentionDays { get; set; }
								