using iotonaspdotnet.Domain.Tenants;
using iotonaspdotnet.Domain.Buildings;
using iotonaspdotnet.Domain.IoTDevices;
using iotonaspdotnet.Domain.Gateways;
using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;
namespace iotonaspdotnet.Domain;

public class Site
{
    public Guid Id { get; set; } = Guid.NewGuid();

public virtual long siteId { get; set; }
 public virtual string name { get; set; }
 public virtual Address address { get; set; }
 public virtual string timezone { get; set; }
 public virtual java.math.BigDecimal latitude { get; set; }
 public virtual java.math.BigDecimal longitude { get; set; }
public virtual Tenant Tenant { get; set; }
public virtual Building Buildings { get; set; }
public virtual IoTDevice Devices { get; set; }
public virtual Gateway Gateways { get; set; }
}
