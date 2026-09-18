using iotonaspdotnet.Domain.Contracts;

namespace iotonaspdotnet.Domain;

public class MessagingEndpoint
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long MessagingendpointId { get; set; }
 public virtual string Host { get; set; }
 public virtual int Port { get; set; }
 public virtual bool Secure { get; set; }
public virtual Tenant Tenant { get; set; }
public virtual TelemetryStream Streams { get; set; }
 public virtual MessagingProtocol Protocol { get; set; }

    public static MessagingEndpoint FromRequest(MessagingEndpointRequest request) {
        return new MessagingEndpoint {
            Id = request.Id,
            Host = request.Host,
            Port = request.Port,
            Secure = request.Secure,
            Protocol = request.Protocol,
        };
    }
}
