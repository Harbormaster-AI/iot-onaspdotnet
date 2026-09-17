using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class Site
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long SiteId { get; set; }
 public virtual string Name { get; set; }
 public virtual Address Address { get; set; }
 public virtual string Timezone { get; set; }
 public virtual decimal Latitude { get; set; }
 public virtual decimal Longitude { get; set; }
public virtual Tenant Tenant { get; set; }
public virtual Building Buildings { get; set; }
public virtual IoTDevice Devices { get; set; }
public virtual Gateway Gateways { get; set; }
}
