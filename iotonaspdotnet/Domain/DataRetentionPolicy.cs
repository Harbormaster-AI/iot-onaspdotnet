using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class DataRetentionPolicy
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual Tenant dataretentionpolicyId { get; set; }
								 public virtual Tenant name { get; set; }
								 public virtual Tenant retentionDays { get; set; }
								public virtual Tenant Tenant { get; set; }
								public virtual TelemetryStream Streams { get; set; }
			}
