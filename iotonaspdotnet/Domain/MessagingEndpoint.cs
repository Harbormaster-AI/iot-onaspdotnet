using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;
namespace iotonaspdotnet.Domain;

public class MessagingEndpoint
{
    public Guid Id { get; set; } = Guid.NewGuid();

public virtual long messagingendpointId { get; set; }
 public virtual string host { get; set; }
 public virtual int port { get; set; }
 public virtual bool secure { get; set; }
public virtual Tenant Tenant { get; set; }
public virtual TelemetryStream Streams { get; set; }
 public virtual MessagingProtocol Protocol { get; set; }
}
