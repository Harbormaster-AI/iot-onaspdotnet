using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class MessagingEndpoint
{
    public Guid Id { get; set; } = Guid.NewGuid();

										 public virtual Tenant messagingendpointId { get; set; }
								 public virtual Tenant host { get; set; }
								 public virtual Tenant port { get; set; }
								 public virtual Tenant secure { get; set; }
								public virtual Tenant Tenant { get; set; }
								public virtual TelemetryStream Streams { get; set; }
								 public virtual TelemetryStream Protocol { get; set; }
			}
