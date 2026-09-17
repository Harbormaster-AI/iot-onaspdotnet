using iotonaspdotnet.Domain.Enums;
using iotonaspdotnet.Domain.ValueObjects;

namespace iotonaspdotnet.Domain;

public class ProvisioningRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();

public virtual long provisioningrecordId { get; set; }
 public virtual DateTime enrolledAt { get; set; }
 public virtual string provisioningService { get; set; }
public virtual IoTDevice Device { get; set; }
public virtual DeviceCertificate Certificate { get; set; }
public virtual Tenant Tenant { get; set; }
 public virtual ProvisioningMethod Method { get; set; }
 public virtual ProvisioningStatus Status { get; set; }
}
