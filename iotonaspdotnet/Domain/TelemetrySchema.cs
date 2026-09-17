using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class TelemetrySchema
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual CommandDefinition telemetryschemaId { get; set; }
								 public virtual CommandDefinition schemaId { get; set; }
								 public virtual CommandDefinition schemaUri { get; set; }
								public virtual TelemetryStream Streams { get; set; }
								 public virtual TelemetryStream Encoding { get; set; }
			}
